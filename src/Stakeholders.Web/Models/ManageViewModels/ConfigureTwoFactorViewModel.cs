// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="ConfigureTwoFactorViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stakeholders.Web.Models.ManageViewModels
{
    /// <summary>
    /// Class ConfigureTwoFactorViewModel.
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// Gets or sets the selected provider.
        /// </summary>
        /// <value>The selected provider.</value>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        public ICollection<SelectListItem> Providers { get; set; }
    }
}
