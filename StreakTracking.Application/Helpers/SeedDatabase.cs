using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper;
using StreakTracking.Application.Contracts.Persistance;

namespace StreakTracking.Application.Helpers;

public class SeedDatabase
{
    private readonly ISqlConnectionService _connection;
    private readonly ILogger<SeedDatabase> _logger;

    public SeedDatabase(ISqlConnectionService connection, ILogger<SeedDatabase> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task CreateTables()
    {
        await using var connection = _connection.GetConnection();
        var query = @"
                        CREATE TABLE IF NOT EXISTS streak(
	                        StreakId UUID PRIMARY KEY,
	                        StreakName VARCHAR(50) NOT NULL,
	                        StreakDescription text NULL,
	                        LongestStreak Integer DEFAULT 0 NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS streak_day(
                            Id DATE  NOT NULL DEFAULT CURRENT_DATE::DATE,
                            Complete BOOLEAN NOT NULL DEFAULT FALSE,
	                        streakid UUID,
	                        CONSTRAINT fk_streak
		                        FOREIGN KEY(streakid)
	   		                        REFERENCES streak(streakid),
                            PRIMARY KEY (streakid, Id)
                        );
                        ";
        try
        {
            await connection.QueryAsync(query);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error while executing query: {error}", e);
        }
    }

    public async Task SeedStreaks()
    {
        
        // Streaks to add to the database
        var streaks = new[]
        {
            new { StreakId = "cd66e042-1eb3-405e-8adc-a4f4062327a8", StreakName="Gym", StreakDescription="Go to the gym 3 times  a week at least", LongestStreak=1 },
            new { StreakId = "11ab7fc6-f037-4086-810c-c14f9c5c8844", StreakName="Japanese", StreakDescription="Study some Japanese every day", LongestStreak=3 },
            new { StreakId = "5488a1a5-af76-4a5b-acd6-aea05a4130da", StreakName="Cold Shower", StreakDescription="Get up in the morning with a cold shower", LongestStreak=4 },
            new { StreakId = "d9082694-ef18-43e8-b7a8-1ac029895513", StreakName="Code", StreakDescription="At least one line of code every day", LongestStreak=5 },
        }.ToList();

        
        // Completion dates for the streaks
        var streakCompletions = new[]
        {
            new {StreakId="cd66e042-1eb3-405e-8adc-a4f4062327a8", completions=new[]
            {
                DateTime.Now,
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(-2),
            }.ToList()},
            new{StreakId="11ab7fc6-f037-4086-810c-c14f9c5c8844", completions=new[]
            {
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(-2),
                DateTime.Now.AddDays(-3),
            }.ToList()},
            new{StreakId="5488a1a5-af76-4a5b-acd6-aea05a4130da", completions=new[]
            {
                DateTime.Now.AddDays(-2),
                DateTime.Now.AddDays(-3),
                DateTime.Now.AddDays(-4),
            }.ToList()},
            new{StreakId="d9082694-ef18-43e8-b7a8-1ac029895513", completions=new[]
            {
                DateTime.Now,
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(-2),
                DateTime.Now.AddDays(-4),
                DateTime.Now.AddDays(-5),
                DateTime.Now.AddDays(-6),
            }.ToList()},
        }.ToList();



        var streakQuery = "INSERT INTO streak(streakid, streakname, streakdescription, longeststreak) " +
                    "VALUES (@StreakId, @StreakName, @StreakDescription, @LongestStreak);";

        var streakCompleteQuery =
            "INSERT INTO streak_day(id, complete, streakid) VALUES (@StreakDate TRUE ,@StreakId);";
        
        await using var connection = _connection.GetConnection();
        try
        {
            foreach (var streak in streaks)
            {
                await connection.QueryAsync(streakQuery, streak);
            }

            foreach (var streak in streakCompletions)
            {
                foreach (var completionDate in streak.completions)
                {
                    await connection.QueryAsync(streakCompleteQuery,
                        new { StreakId = streak.StreakId, StreakDate = completionDate });
                }
            }
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error while executing query: {error}", e);
        }
    }

    public async Task ClearDatabase()
    {
        await using var connection = _connection.GetConnection();
        var query = "DROP TABLE streak_day; DROP TABLE streak";
        try
        {
            await connection.QueryAsync(query);
        }
        catch (PostgresException e)
        {
            _logger.LogError("Error while executing query: {error}", e);
        }
    }
}