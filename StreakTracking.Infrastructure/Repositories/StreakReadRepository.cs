using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;
using StreakTracking.Application.Contracts.Persistance;

namespace StreakTracking.Infrastructure.Repositories;

public class StreakReadRepository : IStreakReadRepository
{
    private readonly ILogger<StreakReadRepository> _logger;
    private readonly ISqlConnectionService<NpgsqlConnection> _connection;

    public StreakReadRepository(ILogger<StreakReadRepository> logger, ISqlConnectionService<NpgsqlConnection> connection)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public async Task<IEnumerable<Streak>> GetStreaks()
    {
        var query = "SELECT * FROM streak";
        
        await using var connection = _connection.GetConnection();
        try
        {
            var streaks = await connection.QueryAsync<Streak>(query);  
            return streaks;
        }
        catch (NpgsqlException e)
        {
            _logger.LogError("Error retrieving streaks from database with Error: {error}", e);
            return new List<Streak>();
        }
    }

    public async Task<Streak> GetStreakById(string Id)
    {
        var parameters = new { StreakId = Id};
        var query = "SELECT * FROM streak WHERE streakid::text = @StreakId";
        
        await using var connection = _connection.GetConnection();
        try
        {
            var result  = await connection.QueryAsync<Streak>(query, parameters);
            var streak = result.FirstOrDefault();
            return streak;
        }
        catch (NpgsqlException e)
        {
            _logger.LogError("Error retrieving streak with id: {streakId} from database with Error: {error}", Id, e);
            return null;
        }
    }

    public async Task<CurrentStreak> GetCurrent(string Id)
    {
        var query = @"
                            SELECT 
                              * 
                            FROM 
                              (
                                SELECT 
                                  Count(*) AS Streak, 
                                  DATE(streak) -1 AS CurrDate 
                                FROM 
                                  (
                                    SELECT 
                                      Id, 
                                      Id + ROW_NUMBER() OVER(
                                        ORDER BY 
                                          Id DESC
                                      ) * interval '1 day' AS Streak 
                                    FROM 
                                      streak_day 
                                    WHERE 
                                      streakid::text = @StreakId
                                  ) AS getconsecutivedates 
                                GROUP BY 
                                  streak
                              ) as groupbydates 
                            WHERE 
                              (
                                CurrDate = CURRENT_DATE 
                                OR CurrDate = CURRENT_DATE - 1
                              ) 
                            ORDER BY 
                              CurrDate DESC 
                            LIMIT 
                              1;
                            ";
        
        var queryParams = new { StreakId = Id };
        
        await using var connection = _connection.GetConnection();
        try
        {
            _logger.LogInformation("Attempting to get current streak of streak with id: {streakId} from the database", Id);
            var result = await connection.QueryAsync<CurrentStreak>(query, queryParams);
            return result.Any() ? result.First() : new CurrentStreak { Streak = 0, CurrDate = DateTime.Today };
        }
        catch (NpgsqlException e)
        {
            _logger.LogError("Error retrieving current streak for streak with id: {streakId}. Error: {e}", Id, e);
            return null;
        }
    }

    public async Task<IEnumerable<StreakDay>> GetCompletions(string id)
    {

        var query =
            "SELECT * FROM streak_day WHERE streakid::text = @StreakId AND complete = true ORDER BY id DESC;";

        var queryParams = new { StreakId = id };
        await using var connection = _connection.GetConnection();
        try
        {
            _logger.LogInformation("Attempting to retrieve completion information for streak with id: {0} ", id);
            var result = await connection.QueryAsync<StreakDay>(query, queryParams);
            return result;
        }
        catch (NpgsqlException e)
        {
            _logger.LogError("Error retrieving streak completions for streak with id: {streakId}. Error: {e}", id, e);
            return null;
        }
    }
}



