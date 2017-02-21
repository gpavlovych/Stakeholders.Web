// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.ActivityViewModels
{
    /// <summary>
    /// Class ActivityViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class ActivityViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        /// <value>The task identifier.</value>
        public long? TaskId { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public long? CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the contact identifier.
        /// </summary>
        /// <value>The contact identifier.</value>
        public long? ContactId { get; set; }

        /// <summary>
        /// Gets or sets the date activity.
        /// </summary>
        /// <value>The date activity.</value>
        public DateTime? DateActivity { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the observer company ids.
        /// </summary>
        /// <value>The observer company ids.</value>
        public long[] ObserverCompanyIds { get; set; }

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
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public long? TypeId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public long? UserId { get; set; }
    }
}