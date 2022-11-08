// <copyright file="EventStoreEventTools.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace CorshamScience.Tools.EventStore
{
    using System;
    using System.Text;
    using global::EventStore.Client;
    using Newtonsoft.Json;

    /// <summary>
    /// A helper class containing methods to handle reading and writing events to/from an <see cref="EventStoreClient"/>.
    /// </summary>
    public static class EventStoreEventTools
    {
        /// <summary>
        /// Convert the provided event <see cref="object"/> to <see cref="EventData"/> which can be saved to an EventStore stream.
        /// </summary>
        /// <param name="eventId">A unique ID for the event.</param>
        /// <param name="event">The event <see cref="object"/> to serialize and encode into <see cref="EventData"/>.</param>
        /// <param name="serializerSettings">Optional <see cref="JsonSerializerSettings"/> to use when serializing the provided <see cref="object"/> and the event metadata.</param>
        /// <returns>A new <see cref="EventData"/> object with the provided ID, and serialized &amp; encoded JSON data, as well as metadata containing the assembly qualified name for the event <see cref="object"/>.</returns>
        // ReSharper disable once UnusedMember.Global
        public static EventData ToEventData(Uuid eventId, object @event, JsonSerializerSettings serializerSettings = null)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, serializerSettings));
            var eventHeaders = new { ClrType = @event.GetType().AssemblyQualifiedName };

            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, serializerSettings));
            var typeName = @event.GetType().Name;

            return new EventData(eventId, typeName, data, metadata);
        }

        /// <summary>
        /// Convert the provided <see cref="ResolvedEvent"/>'s data to the provided <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to convert the <see cref="ResolvedEvent"/>'s data to.</typeparam>
        /// <param name="event">The <see cref="ResolvedEvent"/> whose data should be converted into an <see cref="object"/> of the provided <see cref="Type"/>.</param>
        /// <returns>An <see cref="object"/> of the provided <see cref="Type"/> build from the JSON data contained in the provided <see cref="ResolvedEvent"/>.</returns>
        /// <param name="serializerSettings">Optional <see cref="JsonSerializerSettings"/> to use when deserializing the data in the provided <see cref="ResolvedEvent"/>.</param>
        // ReSharper disable once UnusedMember.Global
        public static T FromResolvedEvent<T>(ResolvedEvent @event, JsonSerializerSettings serializerSettings = null) => (T)FromResolvedEvent(@event, typeof(T), serializerSettings);

        /// <summary>
        /// Convert the provided <see cref="ResolvedEvent"/>'s data to the provided <see cref="Type"/>.
        /// </summary>
        /// <param name="event">The <see cref="ResolvedEvent"/> whose data should be converted into an <see cref="object"/> of the provided <see cref="Type"/>.</param>
        /// <param name="typeToConvertTo">The <see cref="Type"/> to convert the provided <see cref="ResolvedEvent"/>'s data to.</param>
        /// <param name="serializerSettings">Optional <see cref="JsonSerializerSettings"/> to use when deserializing the data in the provided <see cref="ResolvedEvent"/>.</param>
        /// <returns>An <see cref="object"/> of the provided <see cref="Type"/> build from the JSON data contained in the provided <see cref="ResolvedEvent"/>.</returns>
        public static object FromResolvedEvent(ResolvedEvent @event, Type typeToConvertTo, JsonSerializerSettings serializerSettings = null)
        {
            var eventString = Encoding.UTF8.GetString(@event.Event.Data.Span);
            return JsonConvert.DeserializeObject(eventString, typeToConvertTo, serializerSettings);
        }
    }
}
