using Dapper;
using Npgsql;
using StreakTracking.Models;

namespace StreakTracking.Repositories;

public class StreakReadRepository : IStreakReadRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<StreakReadRepository> _logger;

    public StreakReadRepository(IConfiguration configuration, ILogger<StreakReadRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<Streak>> GetStreaks()
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
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

    public async Task<Streak> GetStreakById(string id)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
        try
        {
            var parameters = new { StreakId = id};
            var query = "SELECT * FROM streak WHERE streakid::text = @StreakId";
            var result  = await connection.QueryAsync<Streak>(query, parameters);
            var streak = result.FirstOrDefault();
            return streak;
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error deleting streak with id: {streakId} into database with Error: {error}", Id, e);
            return new Streak();
        }
    }

    public async Task<int> GetCurrent(string Id)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
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
                                  DATE(streak) -1 AS StreakStart 
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
                                      streakid = @StreakId
                                  ) AS getconsecutivedates 
                                GROUP BY 
                                  streak
                              ) as groupbydates 
                            WHERE 
                              (
                                streakstart = CURRENT_DATE 
                                OR streakstart = CURRENT_DATE - 1
                              ) 
                            ORDER BY 
                              streakstart DESC 
                            LIMIT 
                              1;
                            ";
            _logger.LogInformation("Attempting to get current streak of streak with id: {streakId} from the database", Id);
            var result = await connection.QueryAsync(query, queryParams);
            return 2;
        }
        catch (PostgresException e)
        {
            _logger.LogError("");
            return -1;
        }
    }
}



