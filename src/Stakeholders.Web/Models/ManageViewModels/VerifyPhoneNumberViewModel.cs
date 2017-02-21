// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="VerifyPhoneNumberViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Stakeholders.Web.Models.ManageViewModels
{
    /// <summary>
    /// Class VerifyPhoneNumberViewModel.
    /// </summary>
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
