// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="RemoveLoginViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models.ManageViewModels
{
    /// <summary>
    /// Class RemoveLoginViewModel.
    /// </summary>
    public class RemoveLoginViewModel
    {
        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        /// <value>The login provider.</value>
        public string LoginProvider { get; set; }
        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        /// <value>The provider key.</value>
        public string ProviderKey { get; set; }
    }
}
