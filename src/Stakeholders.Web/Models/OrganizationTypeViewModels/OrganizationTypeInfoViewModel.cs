// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="OrganizationTypeInfoViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models.OrganizationTypeViewModels
{
    /// <summary>
    /// Class OrganizationTypeInfoViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.OrganizationTypeViewModels.OrganizationTypeViewModel" />
    public class OrganizationTypeInfoViewModel: OrganizationTypeViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }
    }
}