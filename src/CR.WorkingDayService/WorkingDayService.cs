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
        /// Checks all of the registered IWorkingDaySources and returns true if all of them say that the provided DateTime is on a Working Day.
        /// If there are not IWorkingDaySources registered, all days are considered Working Days.
        /// </summary>
        public bool IsWorkingDay(DateTime date) => _sources == null || _sources.Count == 0 || _sources.All(source => source.IsWorkingDay(date));
    }
}
