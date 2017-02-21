// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="ForgotPasswordViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Stakeholders.Web.Models.AccountViewModels
{
    /// <summary>
    /// Class ForgotPasswordViewModel.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
