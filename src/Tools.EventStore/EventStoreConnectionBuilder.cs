// <copyright file="EventStoreConnectionBuilder.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace CorshamScience.Tools.EventStore
{
    using System;
    using global::EventStore.ClientAPI;

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
        /// <param name="onDisconnected">An optional action to call when the <see cref="IEventStoreConnection"/> disconnects. If no action is passed in, but a logger is, disconnection will result in an error logging event.</param>
        /// <param name="onConnected">An optional action to call when the <see cref="IEventStoreConnection"/> connects. If no action is passed in, but a logger is, connection will result in an info logging event.</param>
        /// <param name="onReconnecting">An optional action to call when the <see cref="IEventStoreConnection"/> is reconnecting. If no action is passed in, but a logger is, reconnection will result in an error logging event.</param>
        /// <param name="onClosed">An optional action to call when the <see cref="IEventStoreConnection"/> is closed. If no action is passed in, but a logger is, closure will result in an info logging event.</param>
        /// <param name="onError">An optional action to call when the <see cref="IEventStoreConnection"/> encounters an error. If no action is passed in, but a logger is, an error occurring will result in an error error logging event.</param>
        /// <returns>A configured, connected <see cref="IEventStoreConnection"/> which uses any passed in configuration variables, or the defaults detailed in the parameters section.</returns>
        // ReSharper disable once UnusedMember.Global
        public static IEventStoreConnection SetUpEventStoreConnection(
            Uri connection,
            int retryLimit = 10,
            TimeSpan? gossipTimeout = null,
            ILogger logger = null,
            Action<object, ClientConnectionEventArgs> onDisconnected = null,
            Action<object, ClientConnectionEventArgs> onConnected = null,
            Action<object, ClientReconnectingEventArgs> onReconnecting = null,
            Action<object, ClientClosedEventArgs> onClosed = null,
            Action<object, ClientErrorEventArgs> onError = null)
        {
            var settings = ConnectionSettings.Create().KeepReconnecting().SetGossipTimeout(gossipTimeout ?? TimeSpan.FromSeconds(5)).LimitRetriesForOperationTo(retryLimit);
            if (logger != null)
            {
                settings.UseCustomLogger(logger);
            }

            var eventStoreClient = EventStoreConnection.Create(settings, connection);

            if (onError != null)
            {
                eventStoreClient.ErrorOccurred += (sender, args) => onError(sender, args);
            }
            else if (logger != null)
            {
                void EventStoreClientOnErrorOccurred(object sender, ClientErrorEventArgs clientErrorEventArgs)
                {
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
            }

            if (onDisconnected != null)
            {
                eventStoreClient.Disconnected += (sender, args) => onDisconnected(sender, args);
            }
            else if (logger != null)
            {
                void EventStoreClientOnDisconnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs) => logger.Error($"Event Store disconnected from endpoint {clientConnectionEventArgs.RemoteEndPoint.Address}:{clientConnectionEventArgs.RemoteEndPoint.Port}");
                eventStoreClient.Disconnected += EventStoreClientOnDisconnected;
            }

            if (onReconnecting != null)
            {
                eventStoreClient.Reconnecting += (sender, args) => onReconnecting(sender, args);
            }
            else if (logger != null)
            {
                void EventStoreClientOnReconnecting(object sender, ClientReconnectingEventArgs clientReconnectingEventArgs) => logger.Info("Event Store reconnecting");
                eventStoreClient.Reconnecting += EventStoreClientOnReconnecting;
            }

            if (onConnected != null)
            {
                eventStoreClient.Connected += (sender, args) => onConnected(sender, args);
            }
            else if (logger != null)
            {
                void EventStoreClientOnConnected(object sender, ClientConnectionEventArgs clientConnectionEventArgs) => logger.Info($"Event Store connected {clientConnectionEventArgs.RemoteEndPoint.Address}:{clientConnectionEventArgs.RemoteEndPoint.Port}");
                eventStoreClient.Connected += EventStoreClientOnConnected;
            }

            if (onClosed != null)
            {
                eventStoreClient.Closed += (sender, args) => onClosed(sender, args);
            }
            else if (logger != null)
            {
                void EventStoreClientOnClosed(object sender, ClientClosedEventArgs clientClosedEventArgs) => logger.Info("Event Store connection closed");
                eventStoreClient.Closed += EventStoreClientOnClosed;
            }

            eventStoreClient.ConnectAsync().Wait();
            return eventStoreClient;
        }
    }
}
