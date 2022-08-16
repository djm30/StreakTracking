using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using StreakTracking.Common.Contracts;
using StreakTracking.Domain.Entities;
using StreakTracking.Worker.Application.Contracts.Persistence;

namespace StreakTracking.Worker.Infrastructure.Repositories;

public class StreakDayWriteRepository : IStreakDayWriteRepository
{
    
    private readonly ILogger<StreakWriteRepository> _logger;
    private readonly ISqlConnectionService<NpgsqlConnection> _connection;

    public StreakDayWriteRepository(ILogger<StreakWriteRepository> logger, ISqlConnectionService<NpgsqlConnection> connection)
    {
        _logger = logger;
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }


    public async Task Create(StreakDay day)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new 
            { 
                Id = day.Id.Date,
                day.Complete,
                day.StreakId
                    
            };
            var query =
                "INSERT INTO streak_day(id, complete, streakid) VALUES (@Id, @Complete , @StreakId);";
            _logger.LogInformation("Attempting to make Streak complete for day: {day} for streak: {streak}", day.Id, day.StreakId);
            await connection.QueryAsync(query, queryParams);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error inserting StreakDay: {streakDay} into database with Error: {error}", day, e);
        }
    }

    public async Task Update(StreakDay day)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new 
            { 
                Id = day.Id.Date,
                Complete = day.Complete,
                StreakId = day.StreakId.ToString()
                    
            };
            var query =
                "UPDATE streak_day SET complete = @Complete WHERE Id = @Id AND streakid::text = @StreakId";
            _logger.LogInformation("Attempting to update streak completion for day: {day} for streak: {streak}", day.Id, day.StreakId);
            await connection.QueryAsync(query, queryParams);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error updating StreakDay: {streakDay} in database with Error: {error}", day, e);
        }
    }

    public async Task Delete(StreakDay day)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new 
            { 
                Id = day.Id.Date,
                StreakId = day.StreakId.ToString()
                    
            };
            var query =
                "DELETE FROM streak_day WHERE Id = @Id AND streakid::text = @StreakId";
            _logger.LogInformation("Attempting to delete streak completion for day: {day} for streak: {streak}", day.Id, day.StreakId);
            await connection.QueryAsync(query, queryParams);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error deleting StreakDay: {streakDay} from database with Error: {error}", day, e);
        }
    }

    public async Task DeleteAll(string streakId)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new 
            { 
                StreakId = streakId
            };
            var query =
                "DELETE FROM streak_day WHERE streakid::text = @StreakId";
            _logger.LogInformation("Attempting to delete all streak completions for streak: {streak}", streakId);
            await connection.QueryAsync(query, queryParams);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error deleting Streak Completion records for streak: {streak} from database with Error: {error}", streakId, e);
        }
    }
}