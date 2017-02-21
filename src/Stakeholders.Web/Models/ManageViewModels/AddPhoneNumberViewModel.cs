// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="AddPhoneNumberViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;

namespace Stakeholders.Web.Models.ManageViewModels
{
    /// <summary>
    /// Class AddPhoneNumberViewModel.
    /// </summary>
    public class AddPhoneNumberViewModel
    {
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
