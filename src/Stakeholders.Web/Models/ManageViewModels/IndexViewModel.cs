// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="IndexViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Stakeholders.Web.Models.ManageViewModels
{
    /// <summary>
    /// Class IndexViewModel.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance has password.
        /// </summary>
        /// <value><c>true</c> if this instance has password; otherwise, <c>false</c>.</value>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Gets or sets the logins.
        /// </summary>
        /// <value>The logins.</value>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [two factor].
        /// </summary>
        /// <value><c>true</c> if [two factor]; otherwise, <c>false</c>.</value>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [browser remembered].
        /// </summary>
        /// <value><c>true</c> if [browser remembered]; otherwise, <c>false</c>.</value>
        public bool BrowserRemembered { get; set; }
    }
}
