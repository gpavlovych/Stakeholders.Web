// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 03-27-2017
//
// Last Modified By : George
// Last Modified On : 03-27-2017
// ***********************************************************************
// <copyright file="TokenRequestViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Stakeholders.Web.Models.TokenViewModels
{
    /// <summary>
    /// Class TokenRequestViewModel.
    /// </summary>
    public class TokenRequestViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
