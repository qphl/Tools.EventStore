// <copyright file="WorkingDayService.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// A Working Day Service which can have IWorkingDaySources registered to it. The service will use all of its registered IWorkingDaySources to determine whether a given DateTime is on a Working Day or Non-Working Day.
    /// </summary>
    public sealed class WorkingDayService : IWorkingDaySource
    {
        private readonly IReadOnlyCollection<IWorkingDaySource> _sources;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingDayService"/> class using the provided IWorkingDaySources to determine whether any given date is on a Working Day, or a Non-Working Day.
        /// </summary>
        /// <param name="sources">The IWorkingDaySources to use to determine whether a given DateTime is on a Working Day, or a Non-Working Day. If no Sources are specified, all days are considered Working Days.</param>
        public WorkingDayService(IReadOnlyCollection<IWorkingDaySource> sources) => _sources = sources;

        /// <inheritdoc />
        /// <summary>
        /// Checks the registered IWorkingDaySources and returns true if any of them say that the provided DateTime is on a Working Day.
        /// If there are not IWorkingDaySources registered, all days are considered Working Days.
        /// </summary>
        public bool IsWorkingDay(DateTime date) => _sources == null || _sources.Count == 0 || _sources.Any(source => source.IsWorkingDay(date));

        /// <summary>
        /// Gets the Next Working Day from the provided DateTime.
        /// </summary>
        /// <param name="date">The DateTime to count from.</param>
        /// <returns>The next Working Day after the provided DateTime.</returns>
        public DateTime NextWorkingDay(DateTime date)
        {
            do
            {
                date = date.AddDays(1);
            }
            while (this.IsNonWorkingDay(date));
            return date;
        }

        /// <summary>
        /// Gets the Previous Working Day from the provided DateTime.
        /// </summary>
        /// <param name="date">The DateTime to count from.</param>
        /// <returns>The last Working Day before the provided DateTime.</returns>
        public DateTime PreviousWorkingDay(DateTime date)
        {
            do
            {
                date = date.AddDays(-1);
            }
            while (this.IsNonWorkingDay(date));
            return date;
        }

        /// <summary>
        /// Add the specified number of working days to the provided DateTime.
        /// </summary>
        /// <param name="numberToAdd">The number of working days to add.</param>
        /// <param name="date">The DateTime to add working days to.</param>
        /// <returns>A DateTime equal to the original DateTime plus however many days need to pass before the specified number of working days have passed.</returns>
        public DateTime AddWorkingDays(uint numberToAdd, DateTime date)
        {
            for (var i = 0; i < numberToAdd; i++)
            {
                do
                {
                    date = date.AddDays(1);
                }
                while (this.IsNonWorkingDay(date));
            }

            return date;
        }

        /// <summary>
        /// Subtract the specified number of working days from the provided DateTime.
        /// </summary>
        /// <param name="numberToSubtract">The number of working days to subtract.</param>
        /// <param name="date">The DateTime to subtract working days from.</param>
        /// <returns>A DateTime equal to the original DateTime minus however many days have passed since it was the specified number of working days away from the provided DateTime.</returns>
        public DateTime SubtractWorkingDays(uint numberToSubtract, DateTime date)
        {
            for (var i = 0; i < numberToSubtract; i++)
            {
                do
                {
                    date = date.AddDays(-1);
                }
                while (this.IsNonWorkingDay(date));
            }

            return date;
        }
    }
}
