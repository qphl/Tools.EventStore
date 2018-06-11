// <copyright file="EventStoreConnectionBuilder.cs" company="Cognisant">
// Copyright (c) Cognisant. All rights reserved.
// </copyright>

namespace Cr.EventStoreTools
{
    using System;
    using EventStore.ClientAPI;

    /// <summary>
    /// A helper class for quickly building an <see cref="IEventStoreConnection"/>.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public static class EventStoreConnectionBuilder
    {
        /// <summary>
        /// Build an <see cref="IEventStoreConnection"/> using the passed in connection <see cref="Uri"/> and configuration.
        /// </summary>
        /// <param name="connection">The URI for the Event Store's external TCP port, including login details.</param>
        /// <param name="retryLimit">An optional maximum allowed retries for any given operation; this defaults to 10.</param>
        /// <param name="gossipTimeout">An optional gossip timeout for the EventStore connection; this defaults to 5 seconds.</param>
        /// <param name="logger">An optional <see cref="ILogger"/> to use to log information about and issues with the <see cref="IEventStoreConnection"/>.</param>
        /// <returns>A configured, connected <see cref="IEventStoreConnection"/> which uses any passed in configuration variables, or the defaults detailed in the parameters section.</returns>
        // ReSharper disable once UnusedMember.Global
        public static IEventStoreConnection SetUpEventStoreConnection(Uri connection, int retryLimit = 10, TimeSpan? gossipTimeout = null, ILogger logger = null)
        {
            var settings = ConnectionSettings.Create().KeepReconnecting().SetGossipTimeout(gossipTimeout ?? TimeSpan.FromSeconds(5)).LimitRetriesForOperationTo(retryLimit);
            if (logger != null)
            {
                settings.UseCustomLogger(logger);
            }

            var eventStoreClient = EventStoreConnection.Create(settings, connection);

            void EventStoreClientOnDisconnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs) => logger?.Error($"Event Store disconnected from endpoint {clientConnectionEventArgs.RemoteEndPoint.Address}:{clientConnectionEventArgs.RemoteEndPoint.Port}");

            void EventStoreClientOnConnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs) => logger?.Info($"Event Store connected {clientConnectionEventArgs.RemoteEndPoint.Address}:{clientConnectionEventArgs.RemoteEndPoint.Port}");

            void EventStoreClientOnReconnecting(object sender, ClientReconnectingEventArgs clientReconnectingEventArgs) => logger?.Info("Event Store reconnecting");

            void EventStoreClientOnClosed(object sender, ClientClosedEventArgs clientClosedEventArgs) => logger?.Info("Event Store connection closed");

            void EventStoreClientOnErrorOccurred(object sender, ClientErrorEventArgs clientErrorEventArgs)
            {
                if (logger == null)
                {
                    return;
                }

                if (clientErrorEventArgs.Exception != null)
                {
                    logger.Error(clientErrorEventArgs.Exception, "Event Store error occurred");
                }
                else
                {
                    logger.Error("Unknown Event Store error");
                }
            }

            eventStoreClient.ErrorOccurred += EventStoreClientOnErrorOccurred;
            eventStoreClient.Disconnected += EventStoreClientOnDisconnected;
            eventStoreClient.Reconnecting += EventStoreClientOnReconnecting;
            eventStoreClient.Connected += EventStoreClientOnConnected;
            eventStoreClient.Closed += EventStoreClientOnClosed;
            eventStoreClient.ConnectAsync().Wait();
            return eventStoreClient;
        }
    }
}
