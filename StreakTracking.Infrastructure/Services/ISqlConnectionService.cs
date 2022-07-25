using Npgsql;

namespace StreakTracking.Infrastructure.Services;

public interface ISqlConnectionService
{
    public NpgsqlConnection GetConnection();
}