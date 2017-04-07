// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
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

        public int ValueProcess { get; set; }
        public double PercentProcess { get; set; }
        public int ValueCompleted { get; set; }
        public double PercentCompleted { get; set; }
        public int ValueReady { get; set; }
        public double PercentReady { get; set; }
    }
}