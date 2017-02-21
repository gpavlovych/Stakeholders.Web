// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="ActivityTaskContact.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class ActivityTaskContact.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class ActivityTaskContact : BaseEntity
    {
        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        /// <value>The task.</value>
        public ActivityTask Task { get; set; }

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>The contact.</value>
        public Contact Contact { get; set; }
    }
}