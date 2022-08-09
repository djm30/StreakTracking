using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StreakTracking.Infrastructure.Services;
using StreakTracking.API.Extensions;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Contracts.Persistance;

namespace StreakTracking.Tests;

public class ApiExtensionMethodsTests
{
    private readonly IServiceCollection _serviceCollection;
    private readonly Dictionary<string, string> _inMemorySettings = new Dictionary<string, string>() { { "ConnectionString", "String" } };

    public ApiExtensionMethodsTests()
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
        
        Assert.IsAssignableFrom<IStreakReadRepository>(provider.GetRequiredService<IStreakReadRepository>());
        Assert.IsAssignableFrom<ISqlConnectionService>(provider.GetRequiredService<ISqlConnectionService>());
    }

    [Fact]
    public void MassTransit_Services_Are_Registered()
    {
        // Arrange
        
        // Act
        _serviceCollection.ConfigureMassTransit();
        
        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        
        //  Checking if a mass transit service which is required for this project is in the service container
        Assert.IsAssignableFrom<IPublishEndpoint>(provider.GetRequiredService<IPublishEndpoint>());
    }

    [Fact]
    public void API_Specific_Services_Are_Registered()
    {
        // Arrange
        //        Adding services these two services depend upon
        var mockStreakRepository = new Mock<IStreakReadRepository>();
        var mockIPublishEndpoint = new Mock<IPublishEndpoint>();
        _serviceCollection.AddScoped<IStreakReadRepository>((service)=> mockStreakRepository.Object);
        _serviceCollection.AddScoped<IPublishEndpoint>((service) => mockIPublishEndpoint.Object);

        // Act
        _serviceCollection.AddServices();
        
        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        Assert.IsAssignableFrom<IMapper>(provider.GetRequiredService<IMapper>());
        Assert.IsAssignableFrom<IStreakReadingService>(provider.GetRequiredService<IStreakReadingService>());
        Assert.IsAssignableFrom<IEventPublishingService>(provider.GetRequiredService<IEventPublishingService>());
    }
}