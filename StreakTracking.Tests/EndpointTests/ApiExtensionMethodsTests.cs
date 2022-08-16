using System.Data.Common;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Npgsql;
using StreakTracking.Common.Contracts;
using StreakTracking.Endpoints.API.Extensions;
using StreakTracking.Endpoints.Application.Contracts.Business;
using StreakTracking.Endpoints.Application.Contracts.Persistence;
using StreakTracking.Worker.Application;
using StreakTracking.Worker.Application.Contracts.Business;
using StreakTracking.Worker.Application.Contracts.Persistence;

namespace StreakTracking.Tests;

public class ApiExtensionMethodsTests
{
    private readonly IServiceCollection _serviceCollection;
    private readonly Dictionary<string, string> _inMemorySettings = new Dictionary<string, string>() { { "ConnectionString", "String" }, {"EventBusConnection", "String"} };
    private readonly IConfiguration _configuration;
        
    public ApiExtensionMethodsTests()
    {
        _serviceCollection = new ServiceCollection();
         _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(_inMemorySettings)
            .Build();
        
        _serviceCollection.AddSingleton<IConfiguration>(_configuration);
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
        Assert.IsAssignableFrom<ISqlConnectionService<NpgsqlConnection>>(provider.GetRequiredService<ISqlConnectionService<NpgsqlConnection>>());
    }

    [Fact]
    public void MassTransit_Services_Are_Registered()
    {
        // Arrange
        
        // Act
        _serviceCollection.ConfigureMassTransit(_configuration);
        
        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        
        //  Checking if a mass transit service which is required for this project is in the service container
        Assert.IsAssignableFrom<IPublishEndpoint>(provider.GetRequiredService<IPublishEndpoint>());
    }

    [Fact]
    public void Applicaton_Services_Are_Registers()
    {
        // Arrange
        
        // Act
        _serviceCollection.AddApplicationServices();
        
        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        
        Assert.IsAssignableFrom<IMapper>(provider.GetRequiredService<IMapper>());
        Assert.IsAssignableFrom<IMediator>(provider.GetRequiredService<IMediator>());
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
        _serviceCollection.AddApplicationServices();

        // Act
        _serviceCollection.AddServices();

        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        Assert.IsAssignableFrom<IStreakReadingService>(provider.GetRequiredService<IStreakReadingService>());
        Assert.IsAssignableFrom<IEventPublishingService>(provider.GetRequiredService<IEventPublishingService>());
    }
}