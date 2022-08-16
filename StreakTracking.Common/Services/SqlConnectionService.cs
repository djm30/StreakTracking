using Microsoft.Extensions.Configuration;
using Npgsql;
using StreakTracking.Common.Contracts;

namespace StreakTracking.Common.Services;
/// <summary>
/// The theory of this class is a class that can be injected to provide the connection
/// to an sql database instead of having the connection logic everywhere
/// I don't know if this is good practice or not but it feels like it could be so idk
/// </summary>
public class SqlConnectionService : ISqlConnectionService<NpgsqlConnection>
{
  private string ConnectionString { get; }
  
  public SqlConnectionService(IConfiguration configuration)
  {
    ConnectionString = configuration.GetValue<string>("ConnectionString");
  }

  public NpgsqlConnection GetConnection() => new(ConnectionString);
}