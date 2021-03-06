﻿// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ApplicationUser.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class ApplicationUser.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser{System.Int64}" />
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser{int}" />
    /// <seealso cref="Stakeholders.Web.Models.IBaseEntity" />
    public class ApplicationUser : IdentityUser<long>, IBaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the observer activities.
        /// </summary>
        /// <value>The observer activities.</value>
        public ICollection<ActivityObserverUser> ObserverActivities { get; set; }

        /// <summary>
        /// Gets or sets the observer activities.
        /// </summary>
        /// <value>The observer activities.</value>
        public ICollection<ActivityTaskObserverUser> ObserverTasks { get; set; }

        /// <summary>
        /// Gets or sets the assigned tasks.
        /// </summary>
        /// <value>The assigned tasks.</value>
        public ICollection<ActivityTask> AssignedTasks { get; set; }

        /// <summary>
        /// Gets or sets the created tasks.
        /// </summary>
        /// <value>The created tasks.</value>
        public ICollection<ActivityTask> CreatedTasks { get; set; }

        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        /// <value>The activities.</value>
        public ICollection<Activity> Activities { get; set; }
        public string PhotoUrl { get; internal set; }
    }
}
