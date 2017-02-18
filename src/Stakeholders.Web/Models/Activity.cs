// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Activity.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Activity
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class Activity: BaseEntity
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>The contact.</value>
        public Contact Contact { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ActivityType Type { get; set; }

        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        /// <value>The task.</value>
        public ActivityTask Task { get; set; }

        /// <summary>
        /// Gets or sets the date activity.
        /// </summary>
        /// <value>The date activity.</value>
        public DateTime DateActivity { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the observer companies.
        /// </summary>
        /// <value>The observer companies.</value>
        public ICollection<Company> ObserverCompanies { get; set; }

        /// <summary>
        /// Gets or sets the observer users.
        /// </summary>
        /// <value>The observer users.</value>
        public ICollection<ApplicationUser> ObserverUsers { get; set; }
    }
}