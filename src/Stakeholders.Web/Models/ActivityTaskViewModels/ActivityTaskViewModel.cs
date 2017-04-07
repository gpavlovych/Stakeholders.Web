// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ActivityTaskViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.ActivityTaskViewModels
{
    /// <summary>
    /// Class ActivityTaskViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class ActivityTaskViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the goal identifier.
        /// </summary>
        /// <value>The goal identifier.</value>
        public long? GoalId { get; set; }

        /// <summary>
        /// Gets or sets the goal title.
        /// </summary>
        /// <value>The goal title.</value>
        public string GoalTitle { get; set; }

        /// <summary>
        /// Gets or sets the assign to identifier.
        /// </summary>
        /// <value>The assign to identifier.</value>
        public long? AssignToId { get; set; }
        /// <summary>
        /// Gets or sets the assign to title.
        /// </summary>
        /// <value>The assign to title.</value>
        public string AssignToTitle { get; set; }
        /// <summary>
        /// Gets or sets the name of the assign to.
        /// </summary>
        /// <value>The name of the assign to.</value>
        public string AssignToName { get; set; }

        /// <summary>
        /// Gets or sets the contact ids.
        /// </summary>
        /// <value>The contact ids.</value>
        public long[] ContactIds { get; set; }

        /// <summary>
        /// Gets or sets the created by identifier.
        /// </summary>
        /// <value>The created by identifier.</value>
        public long? CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the created by title.
        /// </summary>
        /// <value>The created by title.</value>
        public string CreatedByTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the created by.
        /// </summary>
        /// <value>The name of the created by.</value>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Gets or sets the observer user ids.
        /// </summary>
        /// <value>The observer user ids.</value>
        public long[] ObserverUserIds { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

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
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is important.
        /// </summary>
        /// <value><c>true</c> if this instance is important; otherwise, <c>false</c>.</value>
        public bool IsImportant { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>The status identifier.</value>
        public long? StatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>The name of the status.</value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the alias of the status.
        /// </summary>
        /// <value>The alias of the status.</value>
        public string StatusAlias { get; set; }

        /// <summary>
        /// Gets or sets the success factor.
        /// </summary>
        /// <value>The success factor.</value>
        public string SuccessFactor { get; set; }

        /// <summary>
        /// Gets or sets the organization ids.
        /// </summary>
        /// <value>The organization ids.</value>
        public long[] OrganizationIds { get; set; }
    }
}