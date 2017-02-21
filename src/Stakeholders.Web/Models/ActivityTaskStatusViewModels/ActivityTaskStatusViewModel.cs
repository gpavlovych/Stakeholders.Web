﻿// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="ActivityTaskStatusViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.ActivityTaskStatusViewModels
{
    /// <summary>
    /// Class ActivityTaskStatusViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class ActivityTaskStatusViewModel: ViewModelBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        /// <value>The name en.</value>
        public string NameEn { get; set; }
    }
}
