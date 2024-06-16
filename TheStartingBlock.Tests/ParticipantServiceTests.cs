using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TheStartingBlock.Models;
using TheStartingBlock.Contracts;
using TheStartingBlock.Services;
using System.Reflection;

namespace ParticipantUnitTestai
{
    public class ParticipantServiceTests
    {
        Mock<IParticipantRepository> _participantRepoMock;
        IParticipantService participantService;

        public ParticipantServiceTests()
        {
            _participantRepoMock = new Mock<IParticipantRepository>();
            participantService = new ParticipantService(_participantRepoMock.Object);
        }

        [Fact]
        public async Task AddParticipantAsync_Test()
        {
            // Arrange
            var newParticipant = new Participant
            {
                ParticipantId = 1,
                Name = "Jonas Jonaitis",
                PersonalCode = "39704095555",
                BirthYear = 1997,
                Gender = "Male",
            };

            _participantRepoMock.Setup(x => x.AddParticipantAsync(It.IsAny<Participant>())).Returns(Task.CompletedTask);

            // Act
            await participantService.AddParticipantAsync(newParticipant);

            // Assert
            _participantRepoMock.Verify(x => x.AddParticipantAsync(newParticipant), Times.Once);
        }

        [Fact]
        public async Task DeleteParticipantAsync_Test()
        {
            // Arrange
            int participantId = 1;
            _participantRepoMock.Setup(x => x.DeleteParticipantAsync(participantId)).Returns(Task.CompletedTask);

            // Act
            await participantService.DeleteParticipantAsync(participantId);

            // Assert
            _participantRepoMock.Verify(x => x.DeleteParticipantAsync(participantId), Times.Once);
        }

        [Fact]
        public async Task GetParticipantByPersonalIdAsync_Test()
        {
            // Arrange
            string personalId = "JD123456789";
            var newParticipant = new Participant
            {
                ParticipantId = 1,
                Name = "Jonas Jonaitis",
                PersonalCode = "39704095555",
                BirthYear = 1997,
                Gender = "Male",
            };

            _participantRepoMock.Setup(x => x.GetParticipantByPersonalIdAsync(personalId)).ReturnsAsync(newParticipant);

            // Act
            var result = await participantService.GetParticipantByPersonalIdAsync(personalId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newParticipant.ParticipantId, result.ParticipantId);
            Assert.Equal(newParticipant.Name, result.Name);
        }

        [Fact]
        public async Task GetParticipantsAsync_Test()
        {
            // Arrange
            var expectedParticipants = new List<Participant>
            {
                new Participant
                {
                    ParticipantId = 2,
                    Name = "Petras Petraitis",
                    PersonalCode = "39704096666",
                    BirthYear = 1995,
                    Gender = "Male",
                },
                new Participant
                {
                    ParticipantId = 3,
                    Name = "Ona Onutes",
                    PersonalCode = "39704097777",
                    BirthYear = 1959,
                    Gender = "Female",
                }
            };

            _participantRepoMock.Setup(x => x.GetParticipantsAsync()).ReturnsAsync(expectedParticipants);

            // Act
            var result = await participantService.GetParticipantsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedParticipants.Count, result.Count);
        }

        [Fact]
        public async Task UpdateParticipantAsync_Test()
        {
            // Arrange
            var updatedParticipant = new Participant
            {
                ParticipantId = 1,
                Name = "Jonas Jonaitis",
                PersonalCode = "39704095555",
                BirthYear = 1997,
                Gender = "Male",
            };

            _participantRepoMock.Setup(x => x.UpdateParticipantAsync(updatedParticipant)).Returns(Task.CompletedTask);

            // Act
            await participantService.UpdateParticipantAsync(updatedParticipant);

            // Assert
            _participantRepoMock.Verify(x => x.UpdateParticipantAsync(updatedParticipant), Times.Once);
        }
    }
}