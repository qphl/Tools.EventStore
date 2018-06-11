// <copyright file="HttpSourceExtensions.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.HttpSource
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// An Extension Methods class containing methods relating to the HttpWorkingDaySource.
    /// </summary>
    public static class HttpSourceExtensions
    {
        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only a HTTP Source.
        /// </summary>
        /// <typeparam name="T">The Type of the internal state for the HTTP Working Day Source.</typeparam>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="request">The request to make to update the internal state of the HTTP Working Day Source.</param>
        /// <param name="parseAction">The action to build the internal state of the HTTP Working Day Source from the content of the provided HTTP Request's Response.</param>
        /// /// <param name="checkAction">The Action to check the internal state to determine whether a given DateTime is a Working Day or Non-Working Day.</param>
        /// <param name="refreshTimer">The interval on which to make the provided HTTP Request to update the internal state.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder UseHttpSource<T>(this WorkingDayServiceBuilder builder, HttpRequestMessage request, Func<string, T> parseAction, Func<DateTime, T, bool> checkAction, TimeSpan refreshTimer)
            => builder.UseSource(new HttpWorkingDaySource<T>(request, parseAction, checkAction, refreshTimer));

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use a HTTP Source.
        /// </summary>
        /// <typeparam name="T">The Type of the internal state for the HTTP Working Day Source.</typeparam>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="request">The request to make to update the internal state of the HTTP Working Day Source.</param>
        /// <param name="parseAction">The action to build the internal state of the HTTP Working Day Source from the content of the provided HTTP Request's Response.</param>
        /// /// <param name="checkAction">The Action to check the internal state to determine whether a given DateTime is a Working Day or Non-Working Day.</param>
        /// <param name="refreshTimer">The interval on which to make the provided HTTP Request to update the internal state.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder AddHttpSource<T>(this WorkingDayServiceBuilder builder, HttpRequestMessage request, Func<string, T> parseAction, Func<DateTime, T, bool> checkAction, TimeSpan refreshTimer)
            => builder.AddSource(new HttpWorkingDaySource<T>(request, parseAction, checkAction, refreshTimer));
    }
}
