using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using StreakTracking.EventHandler.Services;
using StreakTracking.Domain.Entities;
using StreakTracking.Domain.Calculated;
using StreakTracking.Events.Events;
using StreakTracking.Infrastructure.Repositories;


namespace StreakTracking.Tests;

public class StreakWriteServiceTests
{
    private readonly Mock<IStreakWriteRepository> _mockStreakWriteRepository;
    private readonly Mock<IStreakDayWriteRepository> _mockStreakDayWriteRepository;
    private readonly ILogger<StreakWriteService> _logger;
    private readonly StreakWriteService _sut;
    
    public StreakWriteServiceTests()
    {
        _mockStreakWriteRepository = new Mock<IStreakWriteRepository>();
        _mockStreakDayWriteRepository = new Mock<IStreakDayWriteRepository>();
        _logger = new NullLogger<StreakWriteService>();
        _sut = new StreakWriteService(_mockStreakWriteRepository.Object, _mockStreakDayWriteRepository.Object, _logger);
    }

    [Fact]
    public async Task Create_Streak_Calls_The_Repository()
    {
        // Arrange

        var streakToAdd = new AddStreakEvent()
        {
            StreakId = new Guid(),
            StreakDescription = "Test Description",
            StreakName = "Test Streak"
        };

        // Act
        _sut.CreateStreak(streakToAdd);

        // Assert
        _mockStreakWriteRepository.Verify(m => m.Create(It.IsAny<Streak>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStreak_Calls_The_Repository()
    {
        // Arrange
        var streakToUpdate = new UpdateStreakEvent()
        {
            LongestStreak = 3,
            StreakDescription = "Updated Description",
            StreakId = new Guid(),
            StreakName = "Test Name"
        };

        // Act
        _sut.UpdateStreak(streakToUpdate);

        // Assert
        _mockStreakWriteRepository.Verify(m => m.Update(It.IsAny<Streak>()), Times.Once);
    }
    
    [Fact]
    public async Task Delete_Streak_Calls_The_Repository()
    {
        // Arrange
        var streakToDelete = new DeleteStreakEvent()
        {
            StreakId = new Guid(),
        };
        
        // Act
        _sut.DeleteStreak(streakToDelete);

        // Assert
        _mockStreakWriteRepository.Verify(m => m.Delete(It.IsAny<string>()), Times.Once);
        _mockStreakDayWriteRepository.Verify(m=>m.DeleteAll(It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public async Task MarkComplete_With_True_Adds_To_Repository()
    {
        // Arrange
        var streakToComplete = new StreakCompleteEvent()
        {
            Complete = true,
            Date = new DateTime(2022, 04, 27),
            StreakId = new Guid()
        };

        // Act
        _sut.MarkComplete(streakToComplete);

        // Assert
        _mockStreakDayWriteRepository.Verify(m=>m.Create(It.IsAny<StreakDay>()), Times.Once);
        _mockStreakDayWriteRepository.Verify(m=>m.Delete(It.IsAny<StreakDay>()), Times.Never);
    }
    
    [Fact]
    public async Task MarkComplete_With_False_Deletes_From_Repository()
    {
        // Arrange
        var streakToMarkIncomplete = new StreakCompleteEvent()
        {
            Complete = false,
            Date = new DateTime(2022, 04, 27),
            StreakId = new Guid()
        };
        
        // Act
        _sut.MarkComplete(streakToMarkIncomplete);
        
        // Assert
        _mockStreakDayWriteRepository.Verify(m=>m.Delete(It.IsAny<StreakDay>()), Times.Once);
        _mockStreakDayWriteRepository.Verify(m=>m.Create(It.IsAny<StreakDay>()), Times.Never);
    }
}