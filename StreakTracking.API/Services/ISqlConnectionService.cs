using Npgsql;

namespace StreakTracking.Services;

public interface ISqlConnectionService
{
    public NpgsqlConnection GetConnection();
}