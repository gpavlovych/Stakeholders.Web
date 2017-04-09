// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 04-09-2017
//
// Last Modified By : George
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="PeriodProvider.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Stakeholders.Web
{
    /// <summary>
    /// Class PeriodProvider.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.IPeriodProvider" />
    public class PeriodProvider : IPeriodProvider
    {
        /// <summary>
        /// Starts the of week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="startOfWeek">The start of week.</param>
        /// <returns>DateTime.</returns>
        private DateTime StartOfWeek(DateTime date, DayOfWeek startOfWeek)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return date.AddDays(-1*diff).Date;
        }

        /// <summary>
        /// Gets the this week range.
        /// </summary>
        /// <returns>DateRange.</returns>
        public DateRange GetThisWeekRange()
        {
            var now = DateTime.UtcNow;
            var start = this.StartOfWeek(now, DayOfWeek.Monday);
            var end = start.AddDays(7).AddTicks(-1);
            return new DateRange(minDate: start, maxDate: end);
        }

        /// <summary>
        /// Gets the this month range.
        /// </summary>
        /// <returns>DateRange.</returns>
        public DateRange GetThisMonthRange()
        {
            var now = DateTime.UtcNow;
            var start = new DateTime(now.Year, now.Month, 1);
            var end = start.AddMonths(1).AddTicks(-1);
            return new DateRange(minDate: start, maxDate: end);
        }

        /// <summary>
        /// Gets the this quarter range.
        /// </summary>
        /// <returns>DateRange.</returns>
        public DateRange GetThisQuarterRange()
        {
            var now = DateTime.UtcNow;
            var start = new DateTime(now.Year, (now.Month/3)*3, 1);
            var end = start.AddMonths(3).AddTicks(-1);
            return new DateRange(minDate: start, maxDate: end);
        }

        /// <summary>
        /// Gets the this year range.
        /// </summary>
        /// <returns>DateRange.</returns>
        public DateRange GetThisYearRange()
        {
            var now = DateTime.UtcNow;
            var start = new DateTime(now.Year, 1, 1);
            var end = start.AddYears(1).AddTicks(-1);
            return new DateRange(minDate: start, maxDate: end);
        }
    }
}
