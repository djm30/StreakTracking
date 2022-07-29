using AutoMapper;
using MassTransit;
using StreakTracking.API.Services;
using StreakTracking.API.Models;
using Moq;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Frameworks;
using StreakTracking.Events.Events;
using DependencyInjectionTestingExtensions = MassTransit.Testing.DependencyInjectionTestingExtensions;


namespace StreakTracking.Tests;

public class EventPublisherTests
{
    // Ok this works, I don't really know why but it seems to publish the endpoint or something idk
    [Fact]
    public async Task Valid_Add_Streak_Request_Should_Be_Published()
    {
        // Arrange
        
        // Creating service collection and provider, registering MassTransitInMemoryHarness to it
        var services = new ServiceCollection();
        var provider = services.AddMassTransitInMemoryTestHarness(cfg => {}).BuildServiceProvider(true);
        
        // Creating a scopeFactory so we can get services with a singleton lifetime, which is what IPublishEndpoint needs for this test to run
        var scopeFactory = provider.GetService<IServiceScopeFactory>();
        var harness = provider.GetRequiredService<InMemoryTestHarness>();
        
        // Setting up objects required to test the function
        var id = new Guid();
        
        var addStreakDto = new AddStreakDTO()
        {
            Description = "Test Description",
            Name = "Test Name",
            StreakId = id,
        };

        var addStreakEvent = new AddStreakEvent()
        {
            Description = "Test Description",
            Name = "Test Name",
            StreakId = id,
        };
        
        // Automapper mock setup
        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<AddStreakEvent>(It.IsAny<AddStreakDTO>())).Returns(addStreakEvent);

        // Starting the harness
        await harness.Start();
        
        // Getting a singleton instance of the IPublishEndpoint from the scopedFactory
        var publishEndpoint = scopeFactory.CreateScope().ServiceProvider.GetService<IPublishEndpoint>();
        
        // Creating the instance of the publishing service
        var sut = new EventPublishingService(publishEndpoint, mockMapper.Object);
        
        // Method that will publish the message
        var responseMessage =  await  sut.PublishCreateStreak(addStreakDto);
        
        // Act
        Assert.NotNull(responseMessage);
        
        // Asserting whether the harness published a certain type of event, passed into the gernic paramets
        Assert.True(harness.Published.Select<AddStreakEvent>().Any());
        
        // Stopping the harness after the test has run
        await harness.Stop();
    }
    
    // Try to get IPublishEndpoint working with Moq
    [Fact]
    public async Task Valid_Add_Streak_Request_Should_Be_Published_With_Moq()
    {
        // Arrange
        
        // Creating our publisher mock
        var publishMock = new Mock<IPublishEndpoint>();
        
        // We don't actually need to mock the method here, as it still runs without this implmentation as it doesn't need to return anything
        // But here is how to do it anyway
        publishMock.Setup(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>()));

        
        // Setting up objects required to test the function
        var id = new Guid();

        var addStreakDto = new AddStreakDTO()
        {
            Description = "Test Description",
            Name = "Test Name",
            StreakId = id,
        };

        var addStreakEvent = new AddStreakEvent()
        {
            Description = "Test Description",
            Name = "Test Name",
            StreakId = id,
        };
        
        // Automapper mock setup
        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<AddStreakEvent>(It.IsAny<AddStreakDTO>())).Returns(addStreakEvent);
        
        // Creating the instance of the publishing service
        var sut = new EventPublishingService(publishMock.Object, mockMapper.Object);
        
        // Calling method that will publish the event
        var responseMessage =  await  sut.PublishCreateStreak(addStreakDto);
        
        // Act
        
        // Verifying that the message was published by checking if the metod was called exactly one time over the course of the method.
        publishMock.Verify(p => p.Publish<AddStreakEvent>(It.IsAny<AddStreakEvent>(), It.IsAny<CancellationToken>() ), Times.Once);
    }
}