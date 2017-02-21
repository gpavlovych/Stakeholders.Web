// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="LoginViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Stakeholders.Web.Models.AccountViewModels
{
    /// <summary>
    /// Class LoginViewModel.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
