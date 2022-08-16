using System.Data.Common;

namespace StreakTracking.Common.Contracts;

public interface ISqlConnectionService<T> where T : DbConnection
{
    public T GetConnection();
}