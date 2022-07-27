using System.Net;
using Moq;
using StreakTracking.Domain.Entities;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.API.Services;
using StreakTracking.Domain.Calculated;

namespace StreakTracking.Tests;

public class StreakReadingServiceTests
{
    private readonly Mock<IStreakReadRepository> _mockStreakReadRepository;
    
    public StreakReadingServiceTests()
    {
        _mockStreakReadRepository = new Mock<IStreakReadRepository>();
    }

    [Fact]
    public async Task GetStreakById_Returns_Valid_Streak_Message_And_Status_Code()
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
        
        _mockStreakReadRepository.Setup(s =>
                s.GetStreakById(It.Is<string>(streakId => streakId == id.ToString())))
            .ReturnsAsync(streak);

        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        // Act
        var responseMessage = await streakReadingService.GetStreakById(id.ToString());
        
        // Assert
        Assert.Equal(streak, responseMessage.Content);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Equal("Content Found", responseMessage.Message);
    }

    [Fact]
    public async Task GetStreakById_Returns_Correct_Message_And_Response_Code_For_Not_Found_Streak()
    {
        // Arrange
        var id = new Guid();

        _mockStreakReadRepository.Setup(s =>
                s.GetStreakById(It.Is<string>(streakId => streakId == id.ToString())))
            .ReturnsAsync(()=>null);

        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        
        // Act
        var responseMessage = await streakReadingService.GetStreakById(id.ToString());
        
        // Assert
        var retrievedStreak = responseMessage.Content;
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
        Assert.Equal("No Streak found for this ID", responseMessage.Message);
        Assert.Null(retrievedStreak);
    }

    [Fact]
    public async Task GetStreaks_Returns_Valid_List_Of_Streaks_And_Correct_Response_Message()
    {
        // Arange
        var streaks = new List<Streak>()
        {
            new Streak()
            {
                StreakId = new Guid(),
                LongestStreak = 1,
                StreakDescription = "Test Streak1",
                StreakName = "Test Streak1"
            },
            new Streak()
            {
                StreakId = new Guid(),
                LongestStreak = 2,
                StreakDescription = "Test Streak2",
                StreakName = "Test Streak2"
            },
            new Streak()
            {
                StreakId = new Guid(),
                LongestStreak = 1,
                StreakDescription = "Test Streak3",
                StreakName = "Test Streak3"
            }
        };

        _mockStreakReadRepository.Setup(s => s.GetStreaks()).ReturnsAsync(streaks);
        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        // Act
        var responseMessage = await streakReadingService.GetStreaks();

        // Assert
        var streaksFromService = responseMessage.Content.ToList();
        
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Equal("List of streaks", responseMessage.Message);
        Assert.Equal(streaks, streaksFromService);
    }

    [Fact]
    public async Task GetStreaks_Returns_Empty_Array_And_StatusCodeOK_With_An_Empty_List_Of_Streaks()
    {
        // Arrange
        var streaks = new List<Streak>();
        _mockStreakReadRepository.Setup(s => s.GetStreaks()).ReturnsAsync(streaks);
        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        
        // Act
        var responseMessage = await streakReadingService.GetStreaks();
        
        // Assert
        var streaksFromService = responseMessage.Content.ToList();
        
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Equal("List of streaks", responseMessage.Message);
        Assert.Equal(streaks, streaksFromService);
    }

    [Fact]
    public async Task GetCurrentStreak_Returns_Valid_Current_Streaks_And_Correct_Response_Message()
    {
        // Arrange
        var id = new Guid();
        var currentStreak = new CurrentStreak()
        {
            CurrDate = DateTime.Now,
            Streak = 3,
        };
        _mockStreakReadRepository
            .Setup(s => s.GetCurrent(It.Is<string>((s => s == id.ToString()))))
            .ReturnsAsync(currentStreak);
        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        
        // Act
        var responseMessage = await streakReadingService.GetCurrentStreak(id.ToString());
        
        // Assert
        var currentStreakFromService = responseMessage.Content;
        
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Equal("Current streak calculated", responseMessage.Message);
        Assert.Equal(currentStreak, currentStreakFromService);
    }

    [Fact]
    public async Task GetCurrentStreaks_Returns_Streak_With_0_Days_If_No_Streak()
    {
        var id = new Guid();
        var currentStreak = new CurrentStreak()
        {
            CurrDate = DateTime.Now,
            Streak = 0,
        };
        _mockStreakReadRepository
            .Setup(s => s.GetCurrent(It.Is<string>((s => s == id.ToString()))))
            .ReturnsAsync(currentStreak);
        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        
        // Act
        var responseMessage = await streakReadingService.GetCurrentStreak(id.ToString());
        
        // Assert
        var currentStreakFromService = responseMessage.Content;
        
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        Assert.Equal("Current streak calculated", responseMessage.Message);
        Assert.Equal(currentStreak, currentStreakFromService);
    }
    
    [Fact]
    public async Task GetCurrentStreaks_Returns_Correct_Response_Message_When_There_Is_No_Streak()
    {
        // Arrange
        var id = new Guid();
        _mockStreakReadRepository
            .Setup(s => s.GetCurrent(It.Is<string>((s => s == id.ToString()))))
            .ReturnsAsync(() => null);
        StreakReadingService streakReadingService = new(_mockStreakReadRepository.Object);
        
        // Act
        var responseMessage = await streakReadingService.GetCurrentStreak(id.ToString());
        
        // Assert
        var currentStreakFromService = responseMessage.Content;
        
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
        Assert.Equal("No Streak found for this ID", responseMessage.Message);
        Assert.Null(currentStreakFromService);
    }
}