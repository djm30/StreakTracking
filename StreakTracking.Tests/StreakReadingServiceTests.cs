using System.Net;
using Moq;
using StreakTracking.Domain.Entities;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.API.Services;

namespace StreakTracking.Tests;

public class StreakReadingServiceTests
{
    public StreakReadingServiceTests()
    {
    }

    [Fact]
    public async Task GetStreakById_Returns_Valid_Streak()
    {
        // Arrange
        var id = new Guid();
        var streak = new Streak()
        {
            StreakId = id,
            LongestStreak = 1,
            StreakDescription = "Test Streak",
            StreakName = "Test Streak"
        };

        var MockStreakRepo = new Mock<IStreakReadRepository>();
        MockStreakRepo.Setup(s =>
                s.GetStreakById(It.Is<string>(streakId => streakId == id.ToString())))
            .ReturnsAsync(streak);

        StreakReadingService StreakReadingService = new(MockStreakRepo.Object);
        // Act
        var retrievedStreak = await StreakReadingService.GetStreakById(id.ToString());
        
        // Assert
        Assert.Equal(streak, retrievedStreak.Content);
    }
    
    public async Task GetStreakById_Returns_Correct_Message_And_Response_Code()
    {
        // Arrange
        var id = new Guid();
        var streak = new Streak()
        {
            StreakId = id,
            LongestStreak = 1,
            StreakDescription = "Test Streak",
            StreakName = "Test Streak"
        };

        var MockStreakRepo = new Mock<IStreakReadRepository>();
        MockStreakRepo.Setup(s =>
                s.GetStreakById(It.Is<string>(streakId => streakId == id.ToString())))
            .ReturnsAsync(streak);

        StreakReadingService StreakReadingService = new(MockStreakRepo.Object);
        
        // Act
        var retrievedStreak = await StreakReadingService.GetStreakById(id.ToString());
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, retrievedStreak.StatusCode);
        Assert.Equal("Content Found", retrievedStreak.Message);
    }
}