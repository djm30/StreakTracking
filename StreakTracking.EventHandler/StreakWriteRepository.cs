using System.Threading.Tasks;
using StreakTracking.EventHandler.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace StreakTracking.EventHandler;

public class StreakWriteRepository : IStreakWriteRepository
{

    private readonly IConfiguration _configuration;

    public StreakWriteRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Create(Streak streak)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
        var query = "INSERT INTO streak(streakid, streakname, streakdescription, higheststreak) " +
                    "VALUES (@StreakId, @StreakName, @StreakDescription, @HighestStreak)";
        await connection.QueryAsync(query);
    }

    public async Task Update(Streak streak)
    {
        throw new System.NotImplementedException();
    }

    public async Task Delete(Streak streak)
    {
        throw new System.NotImplementedException();
    }
}