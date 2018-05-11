// <copyright file="DayOfTheWeekSourceExtensions.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.DayOfWeekSource
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An Extension Methods class containing methods relating to the DayOfTheWeekSource.
    /// </summary>
    public static class DayOfTheWeekSourceExtensions
    {
        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only a Day Of The Week Source which considers the provided Week Days to be Working days.
        /// </summary>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="workingDays">The Week Days to consider Working Days.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder UseDayOfTheWeekSource(this WorkingDayServiceBuilder builder, IEnumerable<DayOfWeek> workingDays) => builder.UseSource(new DayOfTheWeekWorkingDaySource(workingDays));

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use a Day Of The Week Source which considers the provided Week Days to be Working days.
        /// </summary>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="workingDays">The Week Days to consider Working Days.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder AddDayOfTheWeekSource(this WorkingDayServiceBuilder builder, IEnumerable<DayOfWeek> workingDays) => builder.AddSource(new DayOfTheWeekWorkingDaySource(workingDays));

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only a Day Of The Week Source which considers the Monday -> Friday to be Working days.
        /// </summary>
        /// <param name="builder">The Builder to configure.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder UseMondayToFridayDayOfTheWeekSource(this WorkingDayServiceBuilder builder) => builder.UseSource(new DayOfTheWeekWorkingDaySource());

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use a Day Of The Week Source which considers the Monday -> Friday to be Working days.
        /// </summary>
        /// <param name="builder">The Builder to configure.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder AddMondayToFridayDayOfTheWeekSource(this WorkingDayServiceBuilder builder) => builder.AddSource(new DayOfTheWeekWorkingDaySource());
    }
}
