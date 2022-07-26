using System.Net;
using AutoMapper;
using MassTransit;
using Moq;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using StreakTracking.Endpoints.Application.Models;
using StreakTracking.Endpoints.Application.Services;
using StreakTracking.Endpoints.Application.Streaks.Commands.AddStreak;
using StreakTracking.Endpoints.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Endpoints.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Endpoints.Application.Streaks.Commands.UpdateStreak;
using StreakTracking.Events.Events;


namespace StreakTracking.Tests;

public class EventPublisherTests
{
    // Ok this works, I don't really know why but it seems to publish the endpoint or something idk
    // public async Task Test_Setup_Harness()
    // {
    //     // Arrange
    //     
    //     // Creating service collection and provider, registering MassTransitInMemoryHarness to it
    //     var services = new ServiceCollection();
    //     var provider = services.AddMassTransitInMemoryTestHarness(cfg => {}).BuildServiceProvider(true);
    //     
    //     // Creating a scopeFactory so we can get services with a singleton lifetime, which is what IPublishEndpoint needs for this test to run
    //     var scopeFactory = provider.GetService<IServiceScopeFactory>();
    //     var harness = provider.GetRequiredService<InMemoryTestHarness>();
    //     
    //     // Setting up objects required to test the function
    //     var id = new Guid();
    //     
    //     var addStreakDto = new AddStreakDTO()
    //     {
    //         StreakDescription = "Test Description",
    //         StreakName = "Test Name",
    //         StreakId = id,
    //     };
    //
    //     var addStreakEvent = new AddStreakEvent()
    //     {
    //         StreakDescription = "Test Description",
    //         StreakName = "Test Name",
    //         StreakId = id,
    //     };
    //     
    //     // Automapper mock setup
    //     var mockMapper = new Mock<IMapper>();
    //     mockMapper.Setup(m => m.Map<AddStreakEvent>(It.IsAny<AddStreakDTO>())).Returns(addStreakEvent);
    //
    //     // Starting the harness
    //     await harness.Start();
    //     
    //     // Getting a singleton instance of the IPublishEndpoint from the scopedFactory
    //     var publishEndpoint = scopeFactory.CreateScope().ServiceProvider.GetService<IPublishEndpoint>();
    //     
    //     // Creating the instance of the publishing service
    //     var sut = new EventPublishingService(publishEndpoint, mockMapper.Object);
    //     
    //     // Method that will publish the message
    //     var responseMessage =  await  sut.PublishCreateStreak(addStreakDto);
    //     
    //     // Act
    //     Assert.NotNull(responseMessage);
    //     
    //     // Asserting whether the harness published a certain type of event, passed into the gernic paramets
    //     Assert.True(harness.Published.Select<AddStreakEvent>().Any());
    //     
    //     // Stopping the harness after the test has run
    //     await harness.Stop();
    // }
    //
    // // Try to get IPublishEndpoint working with Moq
    // public async Task Test_Setup_Mock()
    // {
    //     // Arrange
    //     
    //     // Creating our publisher mock
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     
    //     // We don't actually need to mock the method here, as it still runs without this implmentation as it doesn't need to return anything
    //     // But here is how to do it anyway
    //     publishMock.Setup(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>()));
    //
    //     
    //     // Setting up objects required to test the function
    //     var id = new Guid();
    //
    //     var addStreakDto = new AddStreakDTO()
    //     {
    //         StreakDescription = "Test Description",
    //         StreakName = "Test Name",
    //         StreakId = id,
    //     };
    //
    //     var addStreakEvent = new AddStreakEvent()
    //     {
    //         StreakDescription = "Test Description",
    //         StreakName = "Test Name",
    //         StreakId = id,
    //     };
    //     
    //     // Automapper mock setup
    //     var mockMapper = new Mock<IMapper>();
    //     mockMapper.Setup(m => m.Map<AddStreakEvent>(It.IsAny<AddStreakDTO>())).Returns(addStreakEvent);
    //     
    //     // Creating the instance of the publishing service
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Calling method that will publish the event
    //     var responseMessage =  await  sut.PublishCreateStreak(addStreakDto);
    //     
    //     // Act
    //     
    //     // Verifying that the message was published by checking if the metod was called exactly one time over the course of the method.
    //     publishMock.Verify(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Once);
    // }

    [Fact]
    public async Task Valid_Streak_Add_Event_Is_Published()
    {
        // Arrange
        var publishMock = new Mock<IPublishEndpoint>();
        publishMock.Setup(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>()));
        
        var id = new Guid();

        var addStreakCommand = new AddStreakCommand()
        {
            StreakDescription = "Test Description",
            StreakName = "Test Name",
            StreakId = id,
        };

        var addStreakEvent = new AddStreakEvent()
        {
            StreakDescription = "Test Description",
            StreakName = "Test Name",
            StreakId = id,
        };
        
        var mockMapper = new Mock<IMapper>();
        // mockMapper.Setup(m => m.Map<AddStreakEvent>(It.IsAny<AddStreakCommand>())).Returns(addStreakEvent);
        
        var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
        
        // Act
        await  sut.PublishCreateStreak(addStreakCommand);
        publishMock.Verify(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Once);
    }

    // [Theory]
    // [InlineData("Test Name", "")]
    // [InlineData("", "Test Description")]
    // [InlineData("", "")]
    // public async Task Invalid_Streak_Event_Is_Not_Be_Published(string name,string description)
    // {
    //     // Arrange
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     publishMock.Setup(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>()));
    //     
    //     // Setting up objects required to test the function
    //     var id = new Guid();
    //
    //     var addStreakCommand = new AddStreakCommand()
    //     {
    //         StreakDescription = description,
    //         StreakName = name,
    //         StreakId = id,
    //     };
    //
    //     var addStreakEvent = new AddStreakEvent()
    //     {
    //         StreakDescription = description,
    //         StreakName = name,
    //         StreakId = id,
    //     };
    //     
    //     var mockMapper = new Mock<IMapper>();
    //     mockMapper.Setup(m => m.Map<AddStreakEvent>(It.IsAny<AddStreakCommand>())).Returns(addStreakEvent);
    //     
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Act
    //     await sut.PublishCreateStreak(addStreakCommand);
    //     
    //     // Assert
    //     publishMock.Verify(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Never);
    //
    // }

    [Fact]
    public async Task Valid_Update_Event_Is_Published()
    {
        // Arrange
        var publishMock = new Mock<IPublishEndpoint>();
        publishMock.Setup(p => p.Publish<UpdateStreakEvent>(It.IsAny<UpdateStreakEvent>(), It.IsAny<CancellationToken>()));
        
        var id = new Guid();
        
        var updateStreakCommand = new UpdateStreakCommand()
        {
            LongestStreak = 0,
            StreakDescription = "Test Description",
            StreakName = "Test Name",
            StreakId = id
        };

        var updateStreakEvent = new UpdateStreakEvent()
        {
            LongestStreak = 0,
            StreakDescription = "Test Description",
            StreakName = "Test Name",
            StreakId = id
        };
        
        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<UpdateStreakEvent>(It.IsAny<UpdateStreakCommand>())).Returns(updateStreakEvent);
        
        var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
        
        // Act
        await sut.PublishUpdateStreak(updateStreakCommand);
        
        // Assert
        publishMock.Verify(p => p.Publish<UpdateStreakEvent>(It.IsAny<UpdateStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Once);
    }

    // [Theory]
    // [InlineData("Test Name", "", 1)]
    // [InlineData("", "Test Description", 1)]
    // [InlineData("Test Name", "Test Description", -1)]
    // [InlineData("Test Name", "", -1)]
    // [InlineData("", "Test Description", -1)]
    // [InlineData("", "", 1)]
    // public async Task Invalid_Update_Events_Is_Not_Published(string streakName, string streakDescription, int longestStreak)
    // {
    //     // Arrange
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     publishMock.Setup(p => p.Publish<UpdateStreakEvent>(It.IsAny<UpdateStreakEvent>(), It.IsAny<CancellationToken>()));
    //     
    //     var id = new Guid();
    //     
    //     var updateSteakDto = new UpdateStreakDTO()
    //     {
    //         LongestStreak = longestStreak,
    //         StreakDescription = streakDescription,
    //         StreakName = streakName
    //     };
    //
    //     var updateStreakEvent = new UpdateStreakEvent()
    //     {
    //         LongestStreak = longestStreak,
    //         StreakDescription = streakDescription,
    //         StreakName = streakName,
    //         StreakId = id
    //     };
    //     
    //     var mockMapper = new Mock<IMapper>();
    //     mockMapper.Setup(m => m.Map<UpdateStreakEvent>(It.IsAny<UpdateStreakDTO>())).Returns(updateStreakEvent);
    //     
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Act
    //     var response = await sut.PublishUpdateStreak(id.ToString(), updateSteakDto);
    //     
    //     // Assert
    //     var responseMessageExpected =
    //         "Validation failed: Please provide a name, description and a valid value for the longest streak";
    //     Assert.Equal(responseMessageExpected, response.Message);
    //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //     publishMock.Verify(p => p.Publish<UpdateStreakEvent>(It.IsAny<UpdateStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Never);
    // }

    // [Fact]
    // public async Task Update_Event_With_Invalid_Id_Is_Not_Published()
    // {
    //     // Arrange
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     publishMock.Setup(p => p.Publish<UpdateStreakEvent>(It.IsAny<UpdateStreakEvent>(), It.IsAny<CancellationToken>()));
    //     
    //     var badId = "thisidisntvalidavalidguid";
    //
    //     var mockMapper = new Mock<IMapper>();
    //
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Act
    //     var response = await sut.PublishUpdateStreak(badId, new UpdateStreakDTO());
    //     
    //     // Assert
    //     var responseMessageExpected = "Invalid GUID provided";
    //     Assert.Equal(responseMessageExpected, response.Message);
    //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //     publishMock.Verify(p => p.Publish<UpdateStreakEvent>(It.IsAny<UpdateStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Never);
    // }
    //
    
    [Fact]
    public async Task Valid_Delete_Streak_Event_Is_Published()
    {
        // Arrange
        var publishMock = new Mock<IPublishEndpoint>();
        publishMock.Setup(p => p.Publish<DeleteStreakEvent>(It.IsAny<DeleteStreakEvent>(), It.IsAny<CancellationToken>()));

        var id = new Guid();
        
        var mockMapper = new Mock<IMapper>();

        var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);

        var deleteStreakCommand = new DeleteStreakCommand() { StreakId = id };
        
        // Act
        await sut.PublishDeleteStreak(deleteStreakCommand);

        // Assert
        publishMock.Verify(p => p.Publish<DeleteStreakEvent>(It.IsAny<DeleteStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Once);
    }
    
    // [Fact]
    // public async Task Delete_Event_With_Invalid_Id_Is_Not_Published()
    // {
    //     // Arrange
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     publishMock.Setup(p => p.Publish<DeleteStreakEvent>(It.IsAny<DeleteStreakEvent>(), It.IsAny<CancellationToken>()));
    //
    //     var badId = "thisidisntavalidguid";
    //
    //     var mockMapper = new Mock<IMapper>();
    //     
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Act
    //     var response = await sut.PublishDeleteStreak(badId);
    //     
    //     // Assert
    //     var responseMessageExpected = "Invalid GUID provided";
    //     Assert.Equal(responseMessageExpected, response.Message);
    //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //     publishMock.Verify(p => p.Publish<DeleteStreakEvent>(It.IsAny<DeleteStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Never);
    //
    //     
    // }
     
    [Fact]
    public async Task Valid_Streak_Complete_Event_Is_Published()
    {
        // Arrange
        var publishMock = new Mock<IPublishEndpoint>();
        publishMock.Setup(p => p.Publish<StreakCompleteEvent>(It.IsAny<StreakCompleteEvent>(), It.IsAny<CancellationToken>()));

        var id = new Guid();

        var streakCompleteCommand = new StreakCompleteCommand()
        {
            StreakId = id,
            Complete = true,
            Date = new DateTime(2022, 04, 27),
        };

        var streakCompleteEvent = new StreakCompleteEvent()
        {
            StreakId = id,
            Complete = true,
            Date = new DateTime(2022, 04, 27),
        };
        
        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<StreakCompleteEvent>(It.IsAny<StreakCompleteDTO>())).Returns(streakCompleteEvent);
        
        var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
        
        // Act
        await sut.PublishStreakComplete(streakCompleteCommand);
        
        // Assert
        publishMock.Verify(p => p.Publish<StreakCompleteEvent>(It.IsAny<StreakCompleteEvent>(), It.IsAny<CancellationToken>() ), Times.Once);
    }
    
    // [Fact]
    // public async Task Streak_Complete_Event_With_Future_Date_Is_Not_Published()
    // {
    //     // Arrange
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     publishMock.Setup(p => p.Publish<StreakCompleteEvent>(It.IsAny<StreakCompleteEvent>(), It.IsAny<CancellationToken>()));
    //
    //     var id = new Guid();
    //
    //     var streakCompleteDto = new StreakCompleteDTO()
    //     {
    //         Complete = true,
    //         Date = DateTime.Today.AddDays(1)
    //     };
    //     
    //     var mockMapper = new Mock<IMapper>();
    //     
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Act
    //     var response = await sut.PublishStreakComplete(id.ToString(), streakCompleteDto);
    //     
    //     // Assert
    //     var responseMessageExpected = "Provided date cannot be in the future";
    //     Assert.Equal(responseMessageExpected, response.Message);
    //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //     publishMock.Verify(p => p.Publish<StreakCompleteEvent>(It.IsAny<StreakCompleteEvent>(), It.IsAny<CancellationToken>() ), Times.Never);
    //
    // }
    //
    // [Fact]
    // public async Task Streak_Complete_Event_With_Invalid_Id_Is_Not_Published()
    // {
    //     // Arrange
    //     var publishMock = new Mock<IPublishEndpoint>();
    //     publishMock.Setup(p => p.Publish<StreakCompleteEvent>(It.IsAny<StreakCompleteEvent>(), It.IsAny<CancellationToken>()));
    //     
    //     var badId = "thisisntavalidguid";
    //     
    //     var mockMapper = new Mock<IMapper>();
    //
    //     
    //     var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
    //     
    //     // Act
    //     var response = await sut.PublishStreakComplete(badId, new StreakCompleteDTO());
    //     
    //     // Assert
    //     var responseMessageExpected = "Invalid GUID provided";
    //     Assert.Equal(responseMessageExpected, response.Message);
    //     Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //     publishMock.Verify(p => p.Publish<StreakCompleteEvent>(It.IsAny<StreakCompleteEvent>(), It.IsAny<CancellationToken>() ), Times.Never);
    // }
}