using StreakTracking.Streaks.Models;
using Dapper;
using Npgsql;

namespace StreakTracking.Streaks;

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
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
        var query = "SELECT * FROM streak";
        var streaks = await connection.QueryAsync<Streak>(query);  
        return streaks;
    }

    public async Task<Streak> GetStreakById(string id)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
        var parameters = new { StreakId = id};
        var query = "SELECT * FROM streak WHERE streakid::text = @StreakId";
        var result  = await connection.QueryAsync<Streak>(query, parameters);
        var streak = result.FirstOrDefault();
        return streak;
    }
}