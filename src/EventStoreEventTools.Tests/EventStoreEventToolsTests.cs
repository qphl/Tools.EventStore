namespace EventStoreEventTools.Tests
{
    using NUnit.Framework;
    using EventStore.Client;
    using CorshamScience.Tools.EventStore;
    using Newtonsoft.Json;
    using System.Text;

    internal class EventStoreEventToolsTests
    {
        [Test]
        public void ToEventData_Given_Valid_Event_Returns_EventData()
        {
            var standardEvent = new StandardEvent
            {
                Id = Uuid.NewUuid(),
                Description = "Test",
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(standardEvent));
            var eventHeaders = new { ClrType = standardEvent.GetType().AssemblyQualifiedName };

            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders));
            var typeName = standardEvent.GetType().Name;

            var expectedObject = new EventData(standardEvent.Id, typeName, data, metadata);

            var toEventData = EventStoreEventTools.ToEventData(standardEvent.Id, standardEvent);
            Assert.That(JsonConvert.SerializeObject(toEventData), Is.EqualTo(JsonConvert.SerializeObject(expectedObject)));
        }
        private class StandardEvent
        {
            public Uuid Id { get; set; }
            public string? Description { get; set; }
        }
    }
}
