// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Goal.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class Goal.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.BaseEntity" />
    public class Goal : BaseEntity
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}