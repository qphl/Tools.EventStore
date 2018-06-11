// <copyright file="FileSourceExtensions.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService.FileSource
{
    using System;

    /// <summary>
    /// An Extension Methods class containing methods relating to the FileWorkingDaySource.
    /// </summary>
    public static class FileSourceExtensions
    {
        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only a File Source.
        /// </summary>
        /// <typeparam name="T">The type of the internal state for the File Working Day Source.</typeparam>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="filePath">The path for the file to use for the internal state of the File Working Day Source.</param>
        /// <param name="parseFileContentAction">The action to build the internal state of the File Working Day Source from the content of the provided file.</param>
        /// <param name="checkAction">The action to get whether a given DateTime is on a Working Day based on the current internal State.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder UseFileSource<T>(this WorkingDayServiceBuilder builder, string filePath, Func<string, T> parseFileContentAction, Func<DateTime, T, bool> checkAction)
            => builder.UseSource(new FileWorkingDaySource<T>(filePath, parseFileContentAction, checkAction));

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use a File Source as well as any previously registered Sources.
        /// </summary>
        /// <typeparam name="T">The type of the internal state for the File Working Day Source.</typeparam>
        /// <param name="builder">The Builder to configure.</param>
        /// <param name="filePath">The path for the file to use for the internal state of the File Working Day Source.</param>
        /// <param name="parseFileContentAction">The action to build the internal state of the File Working Day Source from the content of the provided file.</param>
        /// <param name="checkAction">The action to get whether a given DateTime is on a Working Day based on the current internal State.</param>
        /// <returns>The updated WorkingDayServiceBuilder.</returns>
        public static WorkingDayServiceBuilder AddFileSource<T>(this WorkingDayServiceBuilder builder, string filePath, Func<string, T> parseFileContentAction, Func<DateTime, T, bool> checkAction)
            => builder.AddSource(new FileWorkingDaySource<T>(filePath, parseFileContentAction, checkAction));
    }
}
