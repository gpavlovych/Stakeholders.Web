// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 04-09-2017
//
// Last Modified By : George
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="IPeriodProvider.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Stakeholders.Web
{
    /// <summary>
    /// Interface IPeriodProvider
    /// </summary>
    public interface IPeriodProvider
    {
        /// <summary>
        /// Gets the this week range.
        /// </summary>
        /// <returns>DateRange.</returns>
        DateRange GetThisWeekRange();

        /// <summary>
        /// Gets the this month range.
        /// </summary>
        /// <returns>DateRange.</returns>
        DateRange GetThisMonthRange();

        /// <summary>
        /// Gets the this quarter range.
        /// </summary>
        /// <returns>DateRange.</returns>
        DateRange GetThisQuarterRange();

        /// <summary>
        /// Gets the this year range.
        /// </summary>
        /// <returns>DateRange.</returns>
        DateRange GetThisYearRange();
    }
}