using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using TheStartingBlock.Models;
using TheStartingBlock.Contracts;
using TheStartingBlock.Services;
using Serilog;

namespace ResultUnitTestai
{
    public class ResultServiceTests
    {
        private readonly Mock<IResultRepository> _resultRepoMock;
        private readonly ResultService _resultService;

        public ResultServiceTests()
        {
            _resultRepoMock = new Mock<IResultRepository>();
            _resultService = new ResultService(_resultRepoMock.Object);
        }

        [Fact]
        public async Task AddResultAsync_Test()
        {
            // Arrange
            var newResult = new ResultInputModel
            {
                EventId = 1,
                ParticipantId = 1,
                ResultValue = 42.0m
            };

            _resultRepoMock.Setup(x => x.AddResultAsync(It.IsAny<ResultInputModel>()))
                           .Returns(Task.CompletedTask); 

            // Act
            await _resultService.AddResultAsync(newResult);

            // Assert
            _resultRepoMock.Verify(x => x.AddResultAsync(It.IsAny<ResultInputModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteResultAsync_Test()
        {
            // Arrange
            int resultIdToDelete = 1;

            _resultRepoMock.Setup(x => x.DeleteResultAsync(resultIdToDelete))
                           .Returns(Task.CompletedTask); 

            // Act
            await _resultService.DeleteResultAsync(resultIdToDelete);

            // Assert
            _resultRepoMock.Verify(x => x.DeleteResultAsync(resultIdToDelete), Times.Once);
        }

        [Fact]
        public async Task GetResultByIdAsync_Test()
        {
            // Arrange
            int resultId = 1;
            var expectedResult = new Result { ResultId = resultId, ResultValue = 42.0m };

            _resultRepoMock.Setup(x => x.GetResultByIdAsync(resultId))
                           .ReturnsAsync(expectedResult);

            // Act
            var result = await _resultService.GetResultByIdAsync(resultId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(resultId, result.ResultId);
            Assert.Equal(expectedResult.ResultValue, result.ResultValue);
        }

        [Fact]
        public async Task GetResultsAsync_Test()
        {
            // Arrange
            var expectedResults = new List<Result>
            {
                new Result { ResultId = 1, ResultValue = 42.0m },
                new Result { ResultId = 2, ResultValue = 35.5m }
            };

            _resultRepoMock.Setup(x => x.GetResultsAsync())
                           .ReturnsAsync(expectedResults);

            // Act
            var results = await _resultService.GetResultsAsync();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedResults.Count, results.Count);
            Assert.Equal(expectedResults[0].ResultId, results[0].ResultId);
            Assert.Equal(expectedResults[1].ResultId, results[1].ResultId);
        }

        [Fact]
        public async Task UpdateResultAsync_Test()
        {
            // Arrange
            var updatedResult = new ResultInputModel
            {
                ResultId = 1,
                EventId = 1,
                ParticipantId = 1,
                ResultValue = 50.0m
            };

            _resultRepoMock.Setup(x => x.UpdateResultAsync(It.IsAny<ResultInputModel>()))
                           .Returns(Task.CompletedTask); 

            // Act
            await _resultService.UpdateResultAsync(updatedResult);

            // Assert
            _resultRepoMock.Verify(x => x.UpdateResultAsync(It.IsAny<ResultInputModel>()), Times.Once);
        }

        [Fact]
        public async Task GenerateReportAsync_test()
        {
            // Arrange
            _resultRepoMock.Setup(x => x.GenerateReportAsync())
                           .Returns((Task<string>)Task.CompletedTask); 

            // Act
            await _resultService.GenerateReportAsync();

            // Assert
            _resultRepoMock.Verify(x => x.GenerateReportAsync(), Times.Once);
        }
    }
}
