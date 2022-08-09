using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;
using StreakTracking.Infrastructure.Services;
using StreakTracking.Application.Contracts.Persistance;

namespace StreakTracking.Infrastructure.Repositories;

public class StreakReadRepository : IStreakReadRepository
{
    private readonly ILogger<StreakReadRepository> _logger;
    private readonly ISqlConnectionService _connection;

    public StreakReadRepository(ISqlConnectionService connection, ILogger<StreakReadRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<IEnumerable<Streak>> GetStreaks()
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var query = "SELECT * FROM streak";
            var streaks = await connection.QueryAsync<Streak>(query);  
            return streaks;
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error retrieving streaks from database with Error: {error}", e);
            return new List<Streak>();
        }
    }

    public async Task<Streak> GetStreakById(string Id)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var parameters = new { StreakId = Id};
            var query = "SELECT * FROM streak WHERE streakid::text = @StreakId";
            var result  = await connection.QueryAsync<Streak>(query, parameters);
            var streak = result.FirstOrDefault();
            return streak;
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error retrieving streak with id: {streakId} from database with Error: {error}", Id, e);
            return null;
        }
    }

    public async Task<CurrentStreak> GetCurrent(string Id)
    {
        await using var connection = _connection.GetConnection();
        try
        {
            var queryParams = new { StreakId = Id };
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
            _logger.LogInformation("Attempting to get current streak of streak with id: {streakId} from the database", Id);
            var result = await connection.QueryAsync<CurrentStreak>(query, queryParams);
            return result.Any() ? result.First() : new CurrentStreak { Streak = 0, CurrDate = DateTime.Today };
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error retrieving current streak for streak with id: {streakId}. Error: {e}", Id, e);
            return null;
        }
    }
}



