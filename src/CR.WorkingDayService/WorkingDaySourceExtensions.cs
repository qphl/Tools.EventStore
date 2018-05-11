// <copyright file="WorkingDaySourceExtensions.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService
{
    using System;

    /// <summary>
    /// Extension methods for the IWorkingDaySource interface.
    /// </summary>
    public static class WorkingDaySourceExtensions
    {
        /// <summary>
        /// Check with the IWorkingDaySource whether a particular DateTime is on a Non-Working Day.
        /// </summary>
        /// <param name="source">The IWorkingDaySource to use.</param>
        /// <param name="date">The Date Time to check is on a Non-Working Day.</param>
        /// <returns>True if the provided DateTime is a Non-Working Day according to the Source.</returns>
        public static bool IsNonWorkingDay(this IWorkingDaySource source, DateTime date) => !source.IsWorkingDay(date);
    }
}
