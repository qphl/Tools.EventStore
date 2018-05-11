// <copyright file="DayOfTheWeekWorkingDaySource.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.DayOfWeekSource
{
    using System;
    using System.Collections.Generic;

    /// <inheritdoc />
    /// <summary>
    /// An implementation of IWorkingDaySource which uses the Day of the Week of the provided DateTime to determine whether the day is a Working Day or Non-Working Day.
    /// </summary>
    public sealed class DayOfTheWeekWorkingDaySource : IWorkingDaySource
    {
        private static readonly IEnumerable<DayOfWeek> WeekDays = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

        private readonly HashSet<DayOfWeek> _workingDays;

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfTheWeekWorkingDaySource"/> class, using a provided list of Working Days to decide whether a given DateTime is on a Working Day, or Non-Working Day.
        /// </summary>
        /// <param name="workingDays">The Days of the Week to consider Working Days.</param>
        public DayOfTheWeekWorkingDaySource(IEnumerable<DayOfWeek> workingDays) => _workingDays = new HashSet<DayOfWeek>(workingDays);

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfTheWeekWorkingDaySource"/> class, which considers Monday -> Friday Working Days, and Saturday &amp; Sunday Non-Working Days.
        /// </summary>
        public DayOfTheWeekWorkingDaySource()
            : this(WeekDays)
        {
        }

        /// <inheritdoc />
        public bool IsWorkingDay(DateTime date) => _workingDays.Contains(date.DayOfWeek);
    }
}
