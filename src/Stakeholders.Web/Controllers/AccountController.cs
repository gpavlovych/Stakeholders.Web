﻿// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="AccountController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.AccountViewModels;
using Stakeholders.Web.Services;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class AccountController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The sign in manager
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// The SMS sender
        /// </summary>
        private readonly ISmsSender _smsSender;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="smsSender">The SMS sender.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailSender = emailSender;
            this._smsSender = smsSender;
            this._logger = loggerFactory.CreateLogger<AccountController>();
        }

        //
        // GET: /Account/Login
        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        //
        // POST: /Account/Login
        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await this._signInManager.PasswordSignInAsync(
                            model.Email,
                            model.Password,
                            model.RememberMe,
                            lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    this._logger.LogInformation(1, "User logged in.");
                    return this.RedirectToLocal(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return this.RedirectToAction(
                        nameof(SendCode),
                        new
                        {
                            ReturnUrl = returnUrl,
                            RememberMe = model.RememberMe
                        });
                }

                if (result.IsLockedOut)
                {
                    this._logger.LogWarning(2, "User account locked out.");
                    return this.View("Lockout");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // GET: /Account/Register
        /// <summary>
        /// Registers the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await this._userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    this._logger.LogInformation(3, "User created a new account with password.");
                    return this.RedirectToLocal(returnUrl);
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // POST: /Account/LogOff
        /// <summary>
        /// Logs the off.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await this._signInManager.SignOutAsync();
            this._logger.LogInformation(4, "User logged out.");
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //
        // POST: /Account/ExternalLogin
        /// <summary>
        /// Externals the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = this.Url.Action(
                "ExternalLoginCallback",
                "Account",
                new
                {
                    ReturnUrl = returnUrl
                });
            var properties = this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return this.Challenge(properties, provider);
        }

        //
        // GET: /Account/ExternalLoginCallback
        /// <summary>
        /// Externals the login callback.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="remoteError">The remote error.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                this.ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return this.View(nameof(Login));
            }

            var info = await this._signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return this.RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result =
                await this._signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                this._logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return this.RedirectToLocal(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return this.RedirectToAction(
                    nameof(SendCode),
                    new
                    {
                        ReturnUrl = returnUrl
                    });
            }

            if (result.IsLockedOut)
            {
                return this.View("Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                this.ViewData["ReturnUrl"] = returnUrl;
                this.ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return this.View(
                    "ExternalLoginConfirmation",
                    new ExternalLoginConfirmationViewModel
                    {
                        Email = email
                    });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        /// <summary>
        /// Externals the login confirmation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(
            ExternalLoginConfirmationViewModel model,
            string returnUrl = null)
        {
            if (this.ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await this._signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return this.View("ExternalLoginFailure");
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await this._userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this._userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                        this._logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        return this.RedirectToLocal(returnUrl);
                    }
                }

                this.AddErrors(result);
            }

            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View(model);
        }

        // GET: /Account/ConfirmEmail
        /// <summary>
        /// Confirms the email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return this.View("Error");
            }

            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.View("Error");
            }

            var result = await this._userManager.ConfirmEmailAsync(user, code);
            return this.View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        //
        // POST: /Account/ForgotPassword
        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this._userManager.FindByNameAsync(model.Email);
                if (user == null || !(await this._userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return this.View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                //return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        /// <summary>
        /// Forgots the password confirmation.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        //
        // GET: /Account/ResetPassword
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? this.View("Error") : this.View();
        }

        //
        // POST: /Account/ResetPassword
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this._userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }

            var result = await this._userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }

            this.AddErrors(result);
            return this.View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        /// <summary>
        /// Resets the password confirmation.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }

        //
        // GET: /Account/SendCode
        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await this._signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            var userFactors = await this._userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(
                purpose => new SelectListItem
                {
                    Text = purpose,
                    Value = purpose
                }).ToList();
            return this.View(
                new SendCodeViewModel
                {
                    Providers = factorOptions,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                });
        }

        //
        // POST: /Account/SendCode
        /// <summary>
        /// Sends the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this._signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            // Generate the token and send it
            var code = await this._userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return this.View("Error");
            }

            var message = "Your security code is: " + code;
            if (model.SelectedProvider == "Email")
            {
                await this._emailSender.SendEmailAsync(await this._userManager.GetEmailAsync(user), "Security Code", message);
            }
            else if (model.SelectedProvider == "Phone")
            {
                await this._smsSender.SendSmsAsync(await this._userManager.GetPhoneNumberAsync(user), message);
            }

            return this.RedirectToAction(
                nameof(VerifyCode),
                new
                {
                    Provider = model.SelectedProvider,
                    ReturnUrl = model.ReturnUrl,
                    RememberMe = model.RememberMe
                });
        }

        //
        // GET: /Account/VerifyCode
        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await this._signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }

            return this.View(
                new VerifyCodeViewModel
                {
                    Provider = provider,
                    ReturnUrl = returnUrl,
                    RememberMe = rememberMe
                });
        }

        //
        // POST: /Account/VerifyCode
        /// <summary>
        /// Verifies the code.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result =
                await this._signInManager.TwoFactorSignInAsync(
                        model.Provider,
                        model.Code,
                        model.RememberMe,
                        model.RememberBrowser);
            if (result.Succeeded)
            {
                return this.RedirectToLocal(model.ReturnUrl);
            }

            if (result.IsLockedOut)
            {
                this._logger.LogWarning(7, "User account locked out.");
                return this.View("Lockout");
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Invalid code.");
                return this.View(model);
            }
        }

        #region Helpers

        /// <summary>
        /// Adds the errors.
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// Gets the current user asynchronous.
        /// </summary>
        /// <returns>Task&lt;ApplicationUser&gt;.</returns>
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return this._userManager.GetUserAsync(this.HttpContext.User);
        }

        /// <summary>
        /// Redirects to local.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>IActionResult.</returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}