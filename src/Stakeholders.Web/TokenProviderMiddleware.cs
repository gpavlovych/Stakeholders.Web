// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="TokenProviderMiddleware.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stakeholders.Web.Models;

namespace Stakeholders.Web
{
    /// <summary>
    /// Class TokenProviderMiddleware.
    /// </summary>
    public class TokenProviderMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// The options
        /// </summary>
        private readonly TokenProviderOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenProviderMiddleware" /> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="options">The options.</param>
        /// <param name="userManager">The user manager.</param>
        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            UserManager<ApplicationUser> userManager)
        {
            this.next = next;
            this.userManager = userManager;
            this.options = options.Value;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(this.options.Path, StringComparison.Ordinal))
            {
                return this.next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
                || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return this.GenerateToken(context);
        }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <param name="email">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;ClaimsIdentity&gt;.</returns>
        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user =await this.userManager.FindByEmailAsync(email);

            if ((user != null) && await this.userManager.CheckPasswordAsync(user, password))
            {
                return
                    
                        new ClaimsIdentity(
                            new System.Security.Principal.GenericIdentity(email, "Token"),
                            new Claim[] {});
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        private async Task GenerateToken(HttpContext context)
        {
            var email = context.Request.Form["email"];
            var password = context.Request.Form["password"];

            var identity = await this.GetIdentity(email, password);
            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.Ticks.ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: this.options.Issuer,
                audience: this.options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(this.options.Expiration),
                signingCredentials: this.options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int) this.options.Expiration.TotalSeconds
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    response,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    }));
        }
    }
}