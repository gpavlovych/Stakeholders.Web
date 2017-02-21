// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="CompanyViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.CompanyViewModels
{
    /// <summary>
    /// Class CompanyViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class CompanyViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

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
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the observer activity ids.
        /// </summary>
        /// <value>The observer activity ids.</value>
        public long[] ObserverActivityIds { get; set; }

        /// <summary>
        /// Gets or sets the logo URL.
        /// </summary>
        /// <value>The logo URL.</value>
        public string LogoUrl { get; set; }
    }
}
