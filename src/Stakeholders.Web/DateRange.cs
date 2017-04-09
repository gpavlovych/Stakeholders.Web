// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 04-09-2017
//
// Last Modified By : George
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="DateRange.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Stakeholders.Web
{
    /// <summary>
    /// Class DateRange.
    /// </summary>
    public class DateRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        public DateRange(DateTime minDate, DateTime maxDate)
        {
            this.MinDate = minDate;
            this.MaxDate = maxDate;
        }

        /// <summary>
        /// Gets the minimum date.
        /// </summary>
        /// <value>The minimum date.</value>
        public DateTime MinDate { get; }

        /// <summary>
        /// Gets the maximum date.
        /// </summary>
        /// <value>The maximum date.</value>
        public DateTime MaxDate { get; }
    }
}