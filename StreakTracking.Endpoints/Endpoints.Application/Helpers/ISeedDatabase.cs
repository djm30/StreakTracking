namespace StreakTracking.Endpoints.Application.Helpers;

public interface ISeedDatabase
{
    Task CreateTables();
    Task SeedStreaks();
    Task ClearDatabase();
}