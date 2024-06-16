using Xunit;
using Moq;
using System.Threading.Tasks;
using TheStartingBlock.Services;
using TheStartingBlock.Contracts;
using TheStartingBlock.Models;
using System;
using TheStartingBlock.Models.Enums;

namespace EventUnitTestai
{
    public class EventServiceTests
    {
        Mock<IEventRepository> _eventRepoMock;
        IEventService eventService;

        public EventServiceTests()
        {
            _eventRepoMock = new Mock<IEventRepository>();
            eventService = new EventService(_eventRepoMock.Object);
        }

        [Fact]
        public async Task AddEventAsync_Test()
        {
            // Arrange
            var newEvent = new Event {
                EventId = 1,
                Name = "1KM Race",
                StartDate = DateTime.UtcNow,
                Location = "Stadionas",
                PrizeInformation = "100$ Cash prize",
                Category = EventCategory.Mid,
                Type = EventType._1KM,
                Rules = "Rules",
                Requirements = "Requirements"
            };

            // Act
            await eventService.AddEventAsync(newEvent);

            // Assert
            _eventRepoMock.Verify(x => x.AddEventAsync(newEvent), Times.Once);
        }

        [Fact]
        public async Task AddParticipantToEventAsync_Test()
        {
            // Arrange
            int eventId = 1;
            int participantId = 5;

            _eventRepoMock.Setup(x => x.AddParticipantToEventAsync(eventId, participantId)).ReturnsAsync(true);

            // Act
            var result = await eventService.AddParticipantToEventAsync(eventId, participantId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteEventAsync_Test()
        {
            // Arrange
            int eventId = 1;

            // Act
            await eventService.DeleteEventAsync(eventId);

            // Assert
            _eventRepoMock.Verify(x => x.DeleteEventAsync(eventId), Times.Once);
        }

        [Fact]
        public async Task GetEventByIdAsync_Test()
        {
            // Arrange
            int eventId = 1;
            var expectedEvent = new Event {
                EventId = 1,
                Name = "1KM Race",
                StartDate = DateTime.UtcNow,
                Location = "Stadionas",
                PrizeInformation = "100$ Cash prize",
                Category = EventCategory.Mid,
                Type = EventType._1KM,
                Rules = "Rules",
                Requirements = "Requirements"
            };

            _eventRepoMock.Setup(x => x.GetEventByIdAsync(eventId)).ReturnsAsync(expectedEvent);

            // Act
            var result = await eventService.GetEventByIdAsync(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEvent.EventId, result.EventId);
        }

        [Fact]
        public async Task GetEventsAsync_Test()
        {
            // Arrange
            var expectedEvents = new List<Event> { new Event {
                EventId = 1,
                Name = "1KM Race",
                StartDate = DateTime.UtcNow,
                Location = "Stadionas",
                PrizeInformation = "100$ Cash prize",
                Category = EventCategory.Mid,
                Type = EventType._1KM,
                Rules = "Rules",
                Requirements = "Requirements" },

                new Event {
                EventId = 1,
                Name = "10KM Race",
                StartDate = DateTime.UtcNow,
                Location = "Kaunas",
                PrizeInformation = "250$ Cash prize",
                Category = EventCategory.Senior,
                Type = EventType._10KM,
                Rules = "Rules",
                Requirements = "Requirements" },
            };

            _eventRepoMock.Setup(x => x.GetEventsAsync()).ReturnsAsync(expectedEvents);

            // Act
            var result = await eventService.GetEventsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEvents.Count, result.Count); 
        }

        [Fact]
        public async Task UpdateEventAsync_Test()
        {
            // Arrange
            var updatedEvent = new Event {
                EventId = 1,
                Name = "1KM Race",
                StartDate = DateTime.UtcNow,
                Location = "Stadionas",
                PrizeInformation = "100$ Cash prize",
                Category = EventCategory.Mid,
                Type = EventType._1KM,
                Rules = "Rules",
                Requirements = "Requirements"
            };

            // Act
            await eventService.UpdateEventAsync(updatedEvent);

            // Assert
            _eventRepoMock.Verify(x => x.UpdateEventAsync(updatedEvent), Times.Once);
        }
    }
}
