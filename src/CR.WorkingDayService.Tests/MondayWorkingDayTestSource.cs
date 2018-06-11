// <copyright file="MondayWorkingDayTestSource.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.Tests
{
    using System;

    /// <inheritdoc />
    internal sealed class MondayWorkingDayTestSource : IWorkingDaySource
    {
        /// <inheritdoc />
        public bool IsWorkingDay(DateTime date) => date.DayOfWeek == DayOfWeek.Monday;
    }
}
