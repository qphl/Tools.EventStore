// <copyright file="IWorkingDaySource.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService
{
    using System;

    /// <summary>
    /// A Source which can say whether a particular DateTime is on a Working Day or a Non-Working day.
    /// </summary>
    public interface IWorkingDaySource
    {
        /// <summary>
        /// Check with the IWorkingDaySource whether a particular DateTime is on a Working Day.
        /// </summary>
        /// <param name="date">The Date Time to check is on a Working Day.</param>
        /// <returns>True if the provided DateTime is a Working Day according to the Source.</returns>
        bool IsWorkingDay(DateTime date);
    }
}
