// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-18-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="ActivityTask.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Activity Task
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class ActivityTask: BaseEntity
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
        /// Gets or sets the success factor.
        /// </summary>
        /// <value>The success factor.</value>
        public string SuccessFactor { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ActivityTaskStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public ApplicationUser CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the assign to.
        /// </summary>
        /// <value>The assign to.</value>
        public ApplicationUser AssignTo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is important.
        /// </summary>
        /// <value><c>true</c> if this instance is important; otherwise, <c>false</c>.</value>
        public bool IsImportant { get; set; }

        /// <summary>
        /// Gets or sets the date deadline.
        /// </summary>
        /// <value>The date deadline.</value>
        public DateTime DateDeadline { get; set; }

        /// <summary>
        /// Gets or sets the date end.
        /// </summary>
        /// <value>The date end.</value>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the goal.
        /// </summary>
        /// <value>The goal.</value>
        public Goal Goal { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>The contacts.</value>
        public ICollection<ActivityTaskContact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>The contacts.</value>
        public ICollection<ActivityTaskObserverUser> ObserverUsers { get; set; }
    }

    public class ActivityTaskObserverUser: BaseEntity
    {
        public ActivityTask Task { get; set; }
        public ApplicationUser User { get; set; }
    }
}
