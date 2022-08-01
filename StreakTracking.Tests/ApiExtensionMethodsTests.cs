using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StreakTracking.Infrastructure.Services;
using StreakTracking.Infrastructure.Repositories;
using StreakTracking.API.Extensions;

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
        
    }

    [Fact]
    public void API_Specific_Services_Are_Registered()
    {
        
    }
}