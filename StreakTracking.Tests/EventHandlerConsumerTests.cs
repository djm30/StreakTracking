using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StreakTracking.EventHandler.Consumers;
using StreakTracking.EventHandler.Services;
using StreakTracking.Events.Events;

namespace StreakTracking.Tests;

public static class MassTransitHelper
{
    public static IServiceCollection RegisterMassTransitTestConsumer<TConsumer, TEvent>(this IServiceCollection services) 
        where TConsumer: class, IConsumer<TEvent>
        where TEvent: class
    {
        services.AddMassTransitInMemoryTestHarness(cfg =>
        {
            cfg.AddConsumer<TConsumer>();
            cfg.AddConsumerTestHarness<TConsumer>();
        });
        return services;
    }
}

public class EventHandlerConsumerTests
{
    private readonly Mock<IStreakWriteService> _mockStreakWriteService;
    private readonly IServiceCollection _serviceCollection;

    public EventHandlerConsumerTests()
    {
        _mockStreakWriteService = new Mock<IStreakWriteService>();
        _serviceCollection = new ServiceCollection()
            .AddScoped<IStreakWriteService>((service) => _mockStreakWriteService.Object);
    }
    
    [Fact]
    public async Task AddSteakConsumer_Consumes_Published_Event_And_Calls_CreateStreak()
    {
        // Arrange

        var addStreakEvent = new AddStreakEvent()
        {
            StreakDescription = "Test Description",
            StreakId = new Guid(),
            StreakName = "Test Name",
        };

        _serviceCollection.RegisterMassTransitTestConsumer<AddStreakConsumer, AddStreakEvent>();
        
        var provider = _serviceCollection
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<InMemoryTestHarness>();

        await harness.Start();
        try
        {
            var bus = provider.GetRequiredService<IBus>();
            
            // Act
            bus.Publish<AddStreakEvent>(addStreakEvent);

            var consumed = await harness.Consumed.Any<AddStreakEvent>();
            
            // Assert
            Assert.True(consumed);
            _mockStreakWriteService.Verify(m => m.CreateStreak(It.IsAny<AddStreakEvent>()), Times.Once);
        }
        finally
        {
            await harness.Stop();
            await provider.DisposeAsync();
        }
    }

    [Fact]
    public async Task UpdateStreakConsumer_Consumes_Published_Event_And_Calls_UpdateStreak()
    {
        // Arrange

        var updateStreakEvent = new UpdateStreakEvent()
        {
            StreakDescription = "Test Description",
            StreakId = new Guid(),
            StreakName = "Test Name",
            LongestStreak = 3
        };

        _serviceCollection.RegisterMassTransitTestConsumer<UpdateStreakConsumer, UpdateStreakEvent>();
        
        var provider = _serviceCollection
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<InMemoryTestHarness>();

        await harness.Start();
        try
        {
            var bus = provider.GetRequiredService<IBus>();
            
            // Act
            bus.Publish<UpdateStreakEvent>(updateStreakEvent);

            var consumed = await harness.Consumed.Any<UpdateStreakEvent>();
            
            // Assert
            Assert.True(consumed);
            _mockStreakWriteService.Verify(m => m.UpdateStreak(It.IsAny<UpdateStreakEvent>()), Times.Once);
        }
        finally
        {
            await harness.Stop();
            await provider.DisposeAsync();
        }
    }
    
    [Fact]
    public async Task DeleteStreakConsumer_Consumes_Published_Event_And_Calls_DeleteStreak()
    {
        // Arrange

        var deleteStreakEvent = new DeleteStreakEvent()
        {
            StreakId = new Guid(),
        };

        _serviceCollection.RegisterMassTransitTestConsumer<DeleteStreakConsumer, DeleteStreakEvent>();
        
        var provider = _serviceCollection
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<InMemoryTestHarness>();

        await harness.Start();
        try
        {
            var bus = provider.GetRequiredService<IBus>();
            
            // Act
            bus.Publish<DeleteStreakEvent>(deleteStreakEvent);

            var consumed = await harness.Consumed.Any<DeleteStreakEvent>();

            // Assert
            Assert.True(consumed);
            _mockStreakWriteService.Verify(m => m.DeleteStreak(It.IsAny<DeleteStreakEvent>()), Times.Once);
        }
        finally
        {
            await harness.Stop();
            await provider.DisposeAsync();
        }
    }
    
    [Fact]
    public async Task StreakCompleteConsumer_Consumes_Published_Event_And_Calls_MarkComplete()
    {
        // Arrange

        var streakCompleteEvent = new StreakCompleteEvent()
        {
            StreakId = new Guid(),
            Date = new DateTime(2022, 04, 27),
            Complete = true
        };

        _serviceCollection.RegisterMassTransitTestConsumer<StreakCompleteConsumer, StreakCompleteEvent>();
        
        var provider = _serviceCollection
            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<InMemoryTestHarness>();

        await harness.Start();
        try
        {
            var bus = provider.GetRequiredService<IBus>();
            
            // Act
            bus.Publish<StreakCompleteEvent>(streakCompleteEvent);

            var consumed = await harness.Consumed.Any<StreakCompleteEvent>();
            
            // Assert
            Assert.True(consumed);
            _mockStreakWriteService.Verify(m => m.MarkComplete(It.IsAny<StreakCompleteEvent>()), Times.Once);
        }
        finally
        {
            await harness.Stop();
            await provider.DisposeAsync();
        }
    }
}