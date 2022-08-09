using Npgsql;

namespace StreakTracking.Application.Contracts.Persistance;

public interface ISqlConnectionService
{
    public NpgsqlConnection GetConnection();
}