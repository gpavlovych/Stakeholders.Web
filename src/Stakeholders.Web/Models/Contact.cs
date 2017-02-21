// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Contact.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class Contact.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class Contact : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name l.
        /// </summary>
        /// <value>The name l.</value>
        public string NameL { get; set; }

        /// <summary>
        /// Gets or sets the name f.
        /// </summary>
        /// <value>The name f.</value>
        public string NameF { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>The organization.</value>
        public Organization Organization { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the photo URL.
        /// </summary>
        /// <value>The photo URL.</value>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public ICollection<ActivityTaskContact> Tasks { get; set; }
    }
}