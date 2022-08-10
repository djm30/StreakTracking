using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using StreakTracking.Domain.Entities;
using StreakTracking.Infrastructure.Services;
using StreakTracking.Application.Contracts.Persistance;


namespace StreakTracking.Infrastructure.Repositories;

public class StreakWriteRepository : IStreakWriteRepository
{
    private readonly ILogger<StreakWriteRepository> _logger;
    private readonly ISqlConnectionService<NpgsqlConnection> _connection;

    public StreakWriteRepository(ILogger<StreakWriteRepository> logger, ISqlConnectionService<NpgsqlConnection> connection)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public async Task Create(Streak streak)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var query = "INSERT INTO streak(streakid, streakname, streakdescription, longeststreak) " +
                        "VALUES (@StreakId, @StreakName, @StreakDescription, @LongestStreak)";
            _logger.LogInformation("Attempting to write streak: {streak} to the database", streak);
            await connection.QueryAsync(query, streak);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error inserting Streak: {streak} into database with Error: {error}", streak, e);
        }
    }

    public async Task Update(Streak streak)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new
            {
                StreakId = streak.StreakId.ToString(), StreakName = streak.StreakName,
                StreakDescription = streak.StreakDescription
            };
            var query =
                "UPDATE streak SET streakname = @StreakName, streakdescription = @StreakDescription " +
                "WHERE streakid::text = @StreakId;";
            _logger.LogInformation("Attempting to update streak with id: {streakId} to the database", streak.StreakId);
            await connection.QueryAsync(query, queryParams);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error updating Streak: {streak} into database with Error: {error}", streak, e);
        }
    }

    public async Task Delete(string Id)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new { StreakId = Id };
            var query =
                "DELETE FROM streak WHERE streakid::text = @StreakId";
            _logger.LogInformation("Attempting to delete streak with id: {streakId} to the database", Id);
            await connection.QueryAsync(query, queryParams);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error deleting streak with id: {streakId} into database with Error: {error}", Id, e);
        }
    }
}