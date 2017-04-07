// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="GoalViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.GoalViewModels
{
    /// <summary>
    /// Class GoalViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class GoalViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the value process.
        /// </summary>
        /// <value>The value process.</value>
        public long ValueProcess { get; set; }

        /// <summary>
        /// Gets or sets the percent process.
        /// </summary>
        /// <value>The percent process.</value>
        public double PercentProcess { get; set; }

        /// <summary>
        /// Gets or sets the value completed.
        /// </summary>
        /// <value>The value completed.</value>
        public long ValueCompleted { get; set; }

        /// <summary>
        /// Gets or sets the percent completed.
        /// </summary>
        /// <value>The percent completed.</value>
        public double PercentCompleted { get; set; }

        /// <summary>
        /// Gets or sets the value ready.
        /// </summary>
        /// <value>The value ready.</value>
        public long ValueReady { get; set; }

        /// <summary>
        /// Gets or sets the percent ready.
        /// </summary>
        /// <value>The percent ready.</value>
        public double PercentReady { get; set; }
    }
}