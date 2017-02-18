// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Company.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class Company.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class Company: BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the logo URL.
        /// </summary>
        /// <value>The logo URL.</value>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the influencing.
        /// </summary>
        /// <value>The influencing.</value>
        public string Influencing { get; set; }

        /// <summary>
        /// Gets or sets the influenced by.
        /// </summary>
        /// <value>The influenced by.</value>
        public string InfluencedBy { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the observer activities.
        /// </summary>
        /// <value>The observer activities.</value>
        public ICollection<Activity> ObserverActivities { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

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
    }
}