// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-16-2017
// ***********************************************************************
// <copyright file="ManageController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.ManageViewModels;
using Stakeholders.Web.Services;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ManageController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class ManageController : Controller
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
        /// Initializes a new instance of the <see cref="ManageController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="smsSender">The SMS sender.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ManageController(
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
            this._logger = loggerFactory.CreateLogger<ManageController>();
        }

        //
        // GET: /Manage/Index
        /// <summary>
        /// Indexes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            this.ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }
            var model = new IndexViewModel
            {
                HasPassword = await this._userManager.HasPasswordAsync(user),
                PhoneNumber = await this._userManager.GetPhoneNumberAsync(user),
                TwoFactor = await this._userManager.GetTwoFactorEnabledAsync(user),
                Logins = await this._userManager.GetLoginsAsync(user),
                BrowserRemembered = await this._signInManager.IsTwoFactorClientRememberedAsync(user)
            };
            return this.View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        /// <summary>
        /// Removes the login.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel account)
        {
            ManageMessageId? message = ManageMessageId.Error;
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this._userManager.RemoveLoginAsync(user, account.LoginProvider, account.ProviderKey);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    message = ManageMessageId.RemoveLoginSuccess;
                }
            }
            return this.RedirectToAction(nameof(ManageController.ManageLogins), new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        /// <summary>
        /// Adds the phone number.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult AddPhoneNumber()
        {
            return this.View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        /// <summary>
        /// Adds the phone number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            // Generate the token and send it
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }
            var code = await this._userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            await this._smsSender.SendSmsAsync(model.PhoneNumber, "Your security code is: " + code);
            return this.RedirectToAction(nameof(VerifyPhoneNumber), new { PhoneNumber = model.PhoneNumber });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        /// <summary>
        /// Enables the two factor authentication.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableTwoFactorAuthentication()
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                await this._userManager.SetTwoFactorEnabledAsync(user, true);
                await this._signInManager.SignInAsync(user, isPersistent: false);
                this._logger.LogInformation(1, "User enabled two-factor authentication.");
            }
            return this.RedirectToAction(nameof(ManageController.Index), "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        /// <summary>
        /// Disables the two factor authentication.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableTwoFactorAuthentication()
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                await this._userManager.SetTwoFactorEnabledAsync(user, false);
                await this._signInManager.SignInAsync(user, isPersistent: false);
                this._logger.LogInformation(2, "User disabled two-factor authentication.");
            }
            return this.RedirectToAction(nameof(ManageController.Index), "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        /// <summary>
        /// Verifies the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }
            var code = await this._userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
            // Send an SMS to verify the phone number
            return phoneNumber == null ? this.View("Error") : this.View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        /// <summary>
        /// Verifies the phone number.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this._userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Code);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.AddPhoneSuccess });
                }
            }
            // If we got this far, something failed, redisplay the form
            this.ModelState.AddModelError(string.Empty, "Failed to verify phone number");
            return this.View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        /// <summary>
        /// Removes the phone number.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePhoneNumber()
        {
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this._userManager.SetPhoneNumberAsync(user, null);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.RemovePhoneSuccess });
                }
            }
            return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/ChangePassword
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        //
        // POST: /Manage/ChangePassword
        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this._userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    this._logger.LogInformation(3, "User changed their password successfully.");
                    return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                this.AddErrors(result);
                return this.View(model);
            }
            return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.Error });
        }

        //
        // GET: /Manage/SetPassword
        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult SetPassword()
        {
            return this.View();
        }

        //
        // POST: /Manage/SetPassword
        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.GetCurrentUserAsync();
            if (user != null)
            {
                var result = await this._userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }

                this.AddErrors(result);
                return this.View(model);
            }
            return this.RedirectToAction(nameof(ManageController.Index), new { Message = ManageMessageId.Error });
        }

        //GET: /Manage/ManageLogins
        /// <summary>
        /// Manages the logins.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        public async Task<IActionResult> ManageLogins(ManageMessageId? message = null)
        {
            this.ViewData["StatusMessage"] =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.AddLoginSuccess ? "The external login was added."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }
            var userLogins = await this._userManager.GetLoginsAsync(user);
            var otherLogins = this._signInManager.GetExternalAuthenticationSchemes().Where(auth => userLogins.All(ul => auth.AuthenticationScheme != ul.LoginProvider)).ToList();
            this.ViewData["ShowRemoveButton"] = user.PasswordHash != null || userLogins.Count > 1;
            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        /// <summary>
        /// Links the login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = this.Url.Action("LinkLoginCallback", "Manage");
            var properties = this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, this._userManager.GetUserId(this.User));
            return this.Challenge(properties, provider);
        }

        //
        // GET: /Manage/LinkLoginCallback
        /// <summary>
        /// Links the login callback.
        /// </summary>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult> LinkLoginCallback()
        {
            var user = await this.GetCurrentUserAsync();
            if (user == null)
            {
                return this.View("Error");
            }
            var info = await this._signInManager.GetExternalLoginInfoAsync(await this._userManager.GetUserIdAsync(user));
            if (info == null)
            {
                return this.RedirectToAction(nameof(ManageController.ManageLogins), new { Message = ManageMessageId.Error });
            }
            var result = await this._userManager.AddLoginAsync(user, info);
            var message = result.Succeeded ? ManageMessageId.AddLoginSuccess : ManageMessageId.Error;
            return this.RedirectToAction(nameof(ManageController.ManageLogins), new { Message = message });
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
        /// Enum ManageMessageId
        /// </summary>
        public enum ManageMessageId
        {
            /// <summary>
            /// The add phone success
            /// </summary>
            AddPhoneSuccess,
            /// <summary>
            /// The add login success
            /// </summary>
            AddLoginSuccess,
            /// <summary>
            /// The change password success
            /// </summary>
            ChangePasswordSuccess,
            /// <summary>
            /// The set two factor success
            /// </summary>
            SetTwoFactorSuccess,
            /// <summary>
            /// The set password success
            /// </summary>
            SetPasswordSuccess,
            /// <summary>
            /// The remove login success
            /// </summary>
            RemoveLoginSuccess,
            /// <summary>
            /// The remove phone success
            /// </summary>
            RemovePhoneSuccess,
            /// <summary>
            /// The error
            /// </summary>
            Error
        }

        /// <summary>
        /// Gets the current user asynchronous.
        /// </summary>
        /// <returns>Task&lt;ApplicationUser&gt;.</returns>
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return this._userManager.GetUserAsync(this.HttpContext.User);
        }

        #endregion
    }
}
