// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-21-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="ActivityTaskObserverUser.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class ActivityTaskObserverUser.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class ActivityTaskObserverUser : BaseEntity
    {
        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        /// <value>The task.</value>
        public ActivityTask Task { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public ApplicationUser User { get; set; }
    }
}