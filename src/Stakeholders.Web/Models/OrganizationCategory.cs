// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="OrganizationCategory.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class OrganizationCategory.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class OrganizationCategory: BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the influencing.
        /// </summary>
        /// <value>The influencing.</value>
        public string Influencing { get; set; }

        /// <summary>
        /// Gets or sets the influenced by.
        /// </summary>
        /// <value>The influenced by.</value>
        public string InfluencedBy { get; set; }

        /// <summary>
        /// Gets or sets the icon URL.
        /// </summary>
        /// <value>The icon URL.</value>
        public string IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public Company Company { get; set; }
    }
}