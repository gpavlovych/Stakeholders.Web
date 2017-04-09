// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="Organization.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class Organization.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class Organization: BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public OrganizationCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public OrganizationType Type { get; set; }

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
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public ICollection<ActivityTaskOrganization> Tasks { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}