// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="TokenProviderOptions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.IdentityModel.Tokens;

namespace Stakeholders.Web
{
    /// <summary>
    /// Class TokenProviderOptions.
    /// </summary>
    public class TokenProviderOptions
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; } = "/token";

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>The issuer.</value>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>The audience.</value>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>The expiration.</value>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Gets or sets the signing credentials.
        /// </summary>
        /// <value>The signing credentials.</value>
        public SigningCredentials SigningCredentials { get; set; }
    }
}