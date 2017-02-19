// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="GetOrganizationTypesViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models.OrganizationTypeViewModels
{
    /// <summary>
    /// Class GetOrganizationTypesViewModel.
    /// </summary>
    public class GetOrganizationTypesViewModel
    {
        /// <summary>
        /// Gets or sets the organization types.
        /// </summary>
        /// <value>The organization types.</value>
        public OrganizationTypeInfoViewModel[] OrganizationTypes { get; set; }
    }
}