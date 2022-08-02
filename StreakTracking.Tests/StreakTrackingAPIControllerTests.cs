using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using StreakTracking.API.Controllers;
using StreakTracking.API.Models;
using StreakTracking.API.Services;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Tests;

public class StreakTrackingAPIControllerTests
{
    private readonly StreakController _sut;
    private readonly ILogger<StreakController> _mockLogger;
    private readonly Mock<IStreakReadingService> _mockStreakReadService;
    private readonly Mock<IEventPublishingService> _mockPublishingService;
    
    private readonly Guid _knownGuid;

    private readonly IEnumerable<Streak> _streaks = new List<Streak>()
    {
        new Streak()
        {
            StreakId = new Guid(),
            StreakName = "Running",
            StreakDescription = "Run 3k every day",
            LongestStreak = 2,
        },
        new Streak()
        {
            StreakId = new Guid("1059d8d7-c5f4-4ea8-b5af-b08f7f8008b5"),
            StreakName = "Meditating",
            StreakDescription = "Meditate every morning",
            LongestStreak = 7,
        },
        new Streak()
        {
            StreakId = new Guid(),
            StreakName = "Drinking water",
            StreakDescription = "Drink 2 litres of water every day",
            LongestStreak = 10,
        },
        new Streak()
        {
            StreakId = new Guid(),
            StreakName = "Cold Shower",
            StreakDescription = "Take a cold shower everyday, you know how good it is once you get in",
            LongestStreak = 4,
        },
    };

    public StreakTrackingAPIControllerTests()
    {
        _knownGuid = new Guid("1059d8d7-c5f4-4ea8-b5af-b08f7f8008b5");
        _mockLogger = new NullLogger<StreakController>();
        _mockPublishingService = new();
        _mockStreakReadService = new();
        _sut = new(_mockLogger, _mockStreakReadService.Object, _mockPublishingService.Object);
    }

    [Fact]
    public async Task Get_Streaks_Returns_Status_Ok_And_List_Of_Streaks()
    {
        // Arrange
        var responseMessage = new ResponseMessageContent<IEnumerable<Streak>>()
        {
            Message = "List of streaks",
            StatusCode = HttpStatusCode.OK,
            Content = _streaks
        };
        
        _mockStreakReadService.Setup(m => m.GetStreaks()).ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.GetStreaks();
       
        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Streak>>>(response);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(_streaks,okObjectResult.Value);
    }

    [Fact]
    public async Task GetStreakById_Returns_Ok_And_Streak_With_Valid_Id()
    {
        // Arrange
        var streakWithId = _streaks.First(s => s.StreakId == _knownGuid);
        
        var responseMessage = new ResponseMessageContent<Streak>()
        {
            Content = streakWithId,
            Message = "Content Found",
            StatusCode = HttpStatusCode.OK
        };
        
        _mockStreakReadService
            .Setup(m => m.GetStreakById(It.Is<string>(x => x == _knownGuid.ToString())))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.GetStreakById(_knownGuid.ToString());
        
        // Assert
        var actionResult = Assert.IsType<ActionResult<Streak>>(response);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(streakWithId, okObjectResult.Value);
    }

    [Fact]
    public async Task GetStreakById_Returns_BadRequest_With_Error_Message_With_Invalid_Id()
    {
        // Arrange
        var responseMessage = new ResponseMessageContent<Streak>()
        {
            Content = null,
            Message = "No Streak found for this ID",
            StatusCode = HttpStatusCode.NotFound
        };

        _mockStreakReadService
            .Setup(m => m.GetStreakById(It.IsAny<string>())).ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.GetStreakById("notarealstreakid");
        
        // Assert
        var actionResult = Assert.IsType<ActionResult<Streak>>(response);
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        Assert.IsType<ResponseMessageContent<Streak>>(notFoundObjectResult.Value);
        Assert.Equal(responseMessage, notFoundObjectResult.Value);
    }

    [Fact]
    public async Task GetCurrentStreak_Returns_Ok_And_Current_Streak_For_Valid_Streak()
    {
        // Arrange
        var currentStreak = new CurrentStreak()
        {
            CurrDate = DateTime.Now,
            Streak = 6,
        };

        var responseMessage = new ResponseMessageContent<CurrentStreak>()
        {
            Content = currentStreak,
            Message = "Current streak calculated",
            StatusCode = HttpStatusCode.OK,
        };

        _mockStreakReadService
            .Setup(m => m.GetCurrentStreak(It.Is<string>(x => x==_knownGuid.ToString())))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.GetCurrentStreak(_knownGuid.ToString());

        // Assert
        var actionResult = Assert.IsType<ActionResult<CurrentStreak>>(response);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(currentStreak, okObjectResult.Value);
    }

    [Fact]
    public async Task GetCurrentStreak_Returns_NotFound_For_Invalid_Streak()
    {
        // Arrange
        var responseMessage = new ResponseMessageContent<CurrentStreak>()
        {
            Content = null,
            Message = "No Streak found for this ID",
            StatusCode = HttpStatusCode.NotFound
        };
        
        _mockStreakReadService
            .Setup(m => m.GetCurrentStreak(It.IsAny<string>()))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.GetCurrentStreak("notavalidid");
        
        // Assert
        var actionResult = Assert.IsType<ActionResult<CurrentStreak>>(response);
        var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        Assert.Equal(responseMessage, notFoundObjectResult.Value);
    }

    [Fact(Skip = "Target code isn't implemented yet")]
    public async Task GetAllStreakInfo_Returns_Ok_And_All_Info()
    {
        
    }
    
    [Fact(Skip = "Target code isn't implemented yet")]
    public async Task GetFullStreakInfo_Returns_Ok_And_All_Info_For_Valid_Id()
    {
        
    }
    
    [Fact(Skip = "Target code isn't implemented yet")]
    public async Task GetFullStreakInfo_Returns_NotFound_For_Invalid_Id()
    {
        
    }

    [Fact]
    public async Task CreateStreak_Returns_Accepted_For_Valid_Streak()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            StatusCode = HttpStatusCode.Accepted,
            Message = $"Creating new Streak with ID: {_knownGuid.ToString()} ",
        };

        var addStreakDto = new AddStreakDTO()
        {
            StreakName = "Test Streak",
            StreakDescription = "Test Description"
        };

        _mockPublishingService.Setup(m => m.PublishCreateStreak(It.IsAny<AddStreakDTO>()))
            .ReturnsAsync(responseMessage);

        // Act
        var response = await _sut.CreateStreak(addStreakDto);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<AcceptedResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }

    [Fact]
    public async Task CreateStreak_Returns_BadRequest_For_InvalidStreak()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Message = "Validation failed: Please provide both a name and a description",
        };
        
        _mockPublishingService.Setup(m => m.PublishCreateStreak(It.IsAny<AddStreakDTO>()))
            .ReturnsAsync(responseMessage);

        // Act
        var response = await _sut.CreateStreak(new AddStreakDTO());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }

    [Fact]
    public async Task UpdateStreak_Returns_Accepted_For_Valid_Request()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            Message = $"Currently updating Streak with ID: {_knownGuid.ToString()}",
            StatusCode = HttpStatusCode.Accepted,
        };

        _mockPublishingService
            .Setup(m =>
                m.PublishUpdateStreak(It.Is<string>(x => x == _knownGuid.ToString()), It.IsAny<UpdateStreakDTO>()))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.UpdateStreak( _knownGuid.ToString() , new UpdateStreakDTO());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<AcceptedResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }
    
    [Fact]
    public async Task UpdateStreak_Returns_BadRequest_For_Valid_Request()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            Message = "Validation failed: Please provide a name, description and a valid value for the longest streak",
            StatusCode = HttpStatusCode.BadRequest
        };

        _mockPublishingService
            .Setup(m =>
                m.PublishUpdateStreak(It.Is<string>(x => x == _knownGuid.ToString()), It.IsAny<UpdateStreakDTO>()))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.UpdateStreak( _knownGuid.ToString() , new UpdateStreakDTO());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }
    
    [Fact]
    public async Task DeleteStreak_Returns_Accepted_For_Valid_Request()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            Message = $"Currently deleting Streak with ID: {_knownGuid.ToString()}",
            StatusCode = HttpStatusCode.Accepted
        };

        _mockPublishingService
            .Setup(m =>
                m.PublishDeleteStreak(It.Is<string>(x => x == _knownGuid.ToString())))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.DeleteStreak( _knownGuid.ToString());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<AcceptedResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }
    
    [Fact]
    public async Task DeleteStreak_Returns_BadRequest_For_Valid_Request()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            Message = "Invalid GUID provided",
            StatusCode = HttpStatusCode.BadRequest
        };

        _mockPublishingService
            .Setup(m =>
                m.PublishDeleteStreak(It.Is<string>(x => x == _knownGuid.ToString())))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.DeleteStreak( _knownGuid.ToString());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }
    
    [Fact]
    public async Task CompleteStreak_Returns_Accepted_For_Valid_Request()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            Message =
                $"Currently marking Streak with ID: {_knownGuid.ToString()} complete for day: {DateTime.Now}",
            StatusCode = HttpStatusCode.Accepted
        };

        _mockPublishingService
            .Setup(m =>
                m.PublishStreakComplete(It.Is<string>(x => x == _knownGuid.ToString()), It.IsAny<StreakCompleteDTO>()))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.CompleteStreak( _knownGuid.ToString(), new StreakCompleteDTO());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<AcceptedResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }
    
    [Fact]
    public async Task CompleteStreak_Returns_BadRequest_For_Valid_Request()
    {
        // Arrange
        var responseMessage = new ResponseMessage()
        {
            Message = "Invalid GUID provided",
            StatusCode = HttpStatusCode.BadRequest
        };

        _mockPublishingService
            .Setup(m =>
                m.PublishStreakComplete(It.Is<string>(x => x == _knownGuid.ToString()), It.IsAny<StreakCompleteDTO>()))
            .ReturnsAsync(responseMessage);
        
        // Act
        var response = await _sut.CompleteStreak( _knownGuid.ToString(), new StreakCompleteDTO());

        // Assert
        var actionResult = Assert.IsType<ActionResult<ResponseMessage>>(response);
        var objectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(responseMessage, objectResult.Value);
    }
    
}