using System.Data.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Npgsql;
using StreakTracking.Common.Contracts;
using StreakTracking.Worker.Application.Contracts.Business;
using StreakTracking.Worker.Application.Contracts.Persistence;
using StreakTracking.Worker.EventHandler.Consumers;
using StreakTracking.Worker.EventHandler.Extensions;

namespace StreakTracking.Tests;

public class EventHandlerExtensionMethodTests
{
    private readonly IServiceCollection _serviceCollection;
    private readonly Dictionary<string, string> _inMemorySettings = new Dictionary<string, string>() { { "ConnectionString", "String" } };

    public EventHandlerExtensionMethodTests()
    {
        _serviceCollection = new ServiceCollection();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_inMemorySettings)
            .Build();
        
        _serviceCollection.AddSingleton<IConfiguration>(configuration);
        _serviceCollection.AddLogging();
    }


    [Fact]
    public void Infrastructure_Services_Are_Registered()
    {
        // Arrange
        
        // Act
        _serviceCollection.AddInfrastructureServices();

        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        
        Assert.IsAssignableFrom<ISqlConnectionService<NpgsqlConnection>>(provider.GetRequiredService<ISqlConnectionService<NpgsqlConnection>>());
        Assert.IsAssignableFrom<IStreakWriteRepository>(provider.GetRequiredService<IStreakWriteRepository>());
        Assert.IsAssignableFrom<IStreakDayWriteRepository>(provider.GetRequiredService<IStreakDayWriteRepository>());
    }

    [Fact]
    public void MassTransit_Services_Are_Registered()
    {
        // Arrange
        var mockStreakWriteService = Mock.Of<IStreakWriteService>();
        _serviceCollection.AddScoped<IStreakWriteService>((service) => mockStreakWriteService);

        // Act
        _serviceCollection.ConfigureMassTransitConsumers();

        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        
        Assert.IsAssignableFrom<IPublishEndpoint>(provider.GetRequiredService<IPublishEndpoint>());
        Assert.IsType<AddStreakConsumer>(provider.GetRequiredService<AddStreakConsumer>());
        Assert.IsType<UpdateStreakConsumer>(provider.GetRequiredService<UpdateStreakConsumer>());
        Assert.IsType<DeleteStreakConsumer>(provider.GetRequiredService<DeleteStreakConsumer>());
        Assert.IsType<StreakCompleteConsumer>(provider.GetRequiredService<StreakCompleteConsumer>());
    }

    [Fact]
    public void EventHandler_Specific_Services_Are_Registered()
    {
        // Arrange
        
        var mockSqlConnection = Mock.Of<ISqlConnectionService<DbConnection>>();
        var mockStreakWriteRepo = Mock.Of<IStreakWriteRepository>();
        var mockStreakDayWriteRepo = Mock.Of<IStreakDayWriteRepository>();

        _serviceCollection.AddScoped<ISqlConnectionService<DbConnection>>((service) => mockSqlConnection);
        _serviceCollection.AddScoped<IStreakWriteRepository>((service) => mockStreakWriteRepo);
        _serviceCollection.AddScoped<IStreakDayWriteRepository>((service) => mockStreakDayWriteRepo);

        // Act
        _serviceCollection.AddServices();

        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        
        Assert.IsAssignableFrom<ISqlConnectionService<DbConnection>>(provider.GetRequiredService<ISqlConnectionService<DbConnection>>());
    }
}