using System.Data.Common;
using Npgsql;

namespace StreakTracking.Application.Contracts.Persistance;

public interface ISqlConnectionService<T> where T : DbConnection
{
    public T GetConnection();
}