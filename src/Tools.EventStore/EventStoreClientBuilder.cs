// <copyright file="EventStoreClientBuilder.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace CorshamScience.Tools.EventStore
{
    using System;
    using global::EventStore.Client;

    /// <summary>
    /// A helper class for quickly building an <see cref="EventStoreClient"/>.
    /// </summary>
    public static class EventStoreClientBuilder
    {
        /// <summary>
        /// Builds an <see cref="EventStoreClient"/> using the default settings.
        /// </summary>
        /// <param name="connectionString">Put valid connection string.</param>
        /// <returns>EventStoreClient.</returns>
        public static EventStoreClient GetEventStoreClient(string connectionString)
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            return new EventStoreClient(settings);
        }
    }
}
