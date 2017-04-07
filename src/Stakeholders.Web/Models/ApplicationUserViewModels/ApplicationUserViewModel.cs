// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ApplicationUserViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.ApplicationUserViewModels
{
    /// <summary>
    /// Class ApplicationUserViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class ApplicationUserViewModel : ViewModelBase
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
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public long? CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        public long? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>The name of the role.</value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the observer activity identifiers.
        /// </summary>
        /// <value>The observer activity identifiers.</value>
        public long[] ObserverActivityIds { get; set; }

        /// <summary>
        /// Gets or sets the observer tasks.
        /// </summary>
        /// <value>The observer task identifiers.</value>
        public long[] ObserverTaskIds { get; set; }

        /// <summary>
        /// Gets the % of tasks completed.
        /// </summary>
        /// <value>The % of tasks completed.</value>
        public double TasksCompleted { get; internal set; }

        /// <summary>
        /// Gets the activities count.
        /// </summary>
        /// <value>The activities count.</value>
        public long ActivitiesCount { get; internal set; }
    }
}
