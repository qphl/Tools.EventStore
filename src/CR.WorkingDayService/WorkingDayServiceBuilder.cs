// <copyright file="WorkingDayServiceBuilder.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace CR.WorkingDayService
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A Builder Class for the Working Day Service; used for easy configuration.
    /// </summary>
    public sealed class WorkingDayServiceBuilder
    {
        private readonly HashSet<IWorkingDaySource> _sources;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingDayServiceBuilder"/> class.
        /// </summary>
        public WorkingDayServiceBuilder() => _sources = new HashSet<IWorkingDaySource>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkingDayServiceBuilder"/> class.
        /// </summary>
        /// <returns>The new instance of the <see cref="WorkingDayServiceBuilder"/> class.</returns>
        public static WorkingDayServiceBuilder New() => new WorkingDayServiceBuilder();

        /// <summary>
        /// Build a WorkingDayService from the configuration which has been built.
        /// </summary>
        /// <returns>A WorkingDayService with the configured Sources.</returns>
        public WorkingDayService Build() => new WorkingDayService(_sources.ToList());

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use the specified source as well as any previously configured ones.
        /// </summary>
        /// <param name="source">The Source to add (should be unique; adding one source multiple times will only add it once).</param>
        /// <returns>The same instance of the WorkingDayServiceBuilder with all of the previously configured sources, and the new source which was passed in.</returns>
        public WorkingDayServiceBuilder AddSource(IWorkingDaySource source)
        {
            _sources.Add(source);
            return this;
        }

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only the specified source.
        /// </summary>
        /// <param name="source">The Source to use.</param>
        /// <returns>The same instance of the WorkingDayServiceBuilder with all of the previously configured sources replaced with the new source which was passed in.</returns>
        public WorkingDayServiceBuilder UseSource(IWorkingDaySource source)
        {
            _sources.Clear();
            return AddSource(source);
        }

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only the specified sources.
        /// </summary>
        /// <param name="sources">The Sources to use (they should be unique; adding the same source multiple times will result in it only being added once).</param>
        /// <returns>The same instance of the WorkingDayServiceBuilder with its previously configured sources replaced with the ones provided to this method.</returns>
        public WorkingDayServiceBuilder UseSources(params WorkingDayService[] sources) => UseSources((IEnumerable<IWorkingDaySource>)sources);

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use only the specified sources.
        /// </summary>
        /// <param name="sources">The Sources to use (they should be unique; adding the same source multiple times will result in it only being added once).</param>
        /// <returns>The same instance of the WorkingDayServiceBuilder with its previously configured sources replaced with the ones provided to this method.</returns>
        public WorkingDayServiceBuilder UseSources(IEnumerable<IWorkingDaySource> sources)
        {
            _sources.Clear();
            return AddSources(sources);
        }

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use the specified sources as well as any previously configured ones.
        /// </summary>
        /// <param name="sources">The Sources to add (they should be unique; adding the same source multiple times will result in it only being added once).</param>
        /// <returns>The same instance of the WorkingDayServiceBuilder with its previously configured sources as well as the ones provided to this method.</returns>
        public WorkingDayServiceBuilder AddSources(params WorkingDayService[] sources) => AddSources((IEnumerable<IWorkingDaySource>)sources);

        /// <summary>
        /// Configure the WorkingDayServiceBuilder to use the specified sources as well as any previously configured ones.
        /// </summary>
        /// <param name="sources">The Sources to add (they should be unique; adding the same source multiple times will result in it only being added once).</param>
        /// <returns>The same instance of the WorkingDayServiceBuilder with its previously configured sources as well as the ones provided to this method.</returns>
        public WorkingDayServiceBuilder AddSources(IEnumerable<IWorkingDaySource> sources)
        {
            foreach (var source in sources)
            {
                _sources.Add(source);
            }

            return this;
        }
    }
}
