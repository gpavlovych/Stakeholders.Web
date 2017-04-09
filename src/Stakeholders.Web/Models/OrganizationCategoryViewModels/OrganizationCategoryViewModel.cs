// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 04-08-2017
// ***********************************************************************
// <copyright file="OrganizationCategoryViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.OrganizationCategoryViewModels
{
    /// <summary>
    /// Class OrganizationCategoryViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class OrganizationCategoryViewModel : ViewModelBase
    {
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
        /// Gets or sets the icon URL.
        /// </summary>
        /// <value>The icon URL.</value>
        public string IconUrl { get; set; }

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
        /// Gets or sets the tasks completed percentage.
        /// </summary>
        /// <value>The tasks completed percentage.</value>
        public double TasksCompletedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the tasks number.
        /// </summary>
        /// <value>The tasks number.</value>
        public long TasksNumber { get; set; }

        /// <summary>
        /// Gets or sets the activities number.
        /// </summary>
        /// <value>The activities number.</value>
        public long ActivitiesNumber { get; set; }
    }
}