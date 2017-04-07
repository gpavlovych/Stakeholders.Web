// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 04-04-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ActivityObserverCompany.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class ActivityObserverCompany.
    /// </summary>
    public class ActivityObserverCompany
    {
        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        /// <value>The activity identifier.</value>
        public long ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        public Activity Activity { get; set; }

        /// <summary>
        /// Gets or sets the observer company identifier.
        /// </summary>
        /// <value>The observer company identifier.</value>
        public long CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the observer company.
        /// </summary>
        /// <value>The observer company.</value>
        public Company Company { get; set; }
    }
}