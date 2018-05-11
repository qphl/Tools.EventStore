// <copyright file="StringSourceExtensions.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.StringSource
{
    using System;

    /// <summary>
    /// An Extension Methods class containing methods relating to the StringWorkingDaySource.
    /// </summary>
    public static class StringSourceExtensions
    {
        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only a String Source.
        /// </summary>
        /// <typeparam name="T">The type of the internal state for the String Working Day Source.</typeparam>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="source">The path for the file to use for the internal state of the String Working Day Source.</param>
        /// <param name="parseFileContentAction">The action to build the internal state of the String Working Day Source from the content of the Source.</param>
        /// <param name="checkAction">The action to check whether a given DateTime is a Working Day based on the current State of the String Working Day Source.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder UseStringSource<T>(this WorkingDayServiceBuilder builder, string source, Func<string, T> parseFileContentAction, Func<DateTime, T, bool> checkAction)
            => builder.UseSource(new StringWorkingDaySource<T>(source, parseFileContentAction, checkAction));

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use a String Source as well as any previously registered Sources.
        /// </summary>
        /// <typeparam name="T">The type of the internal state for the String Working Day Source.</typeparam>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="source">The path for the file to use for the internal state of the String Working Day Source.</param>
        /// <param name="parseFileContentAction">The action to build the internal state of the String Working Day Source from the content of the Source.</param>
        /// <param name="checkAction">The action to check whether a given DateTime is a Working Day based on the current State of the String Working Day Source.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder AddStringSource<T>(this WorkingDayServiceBuilder builder, string source, Func<string, T> parseFileContentAction, Func<DateTime, T, bool> checkAction)
            => builder.AddSource(new StringWorkingDaySource<T>(source, parseFileContentAction, checkAction));
    }
}
