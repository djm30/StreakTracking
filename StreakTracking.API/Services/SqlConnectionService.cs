using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace StreakTracking.Services;
/// <summary>
/// The theory of this class is a class that can be injected to provide the connection
/// to an sql database instead of having the connection logic everywhere
/// I don't know if this is good practice or not but it feels like it could be so idk
/// </summary>
public class SqlConnectionService : ISqlConnectionService
{
  private IConfiguration _configuration;
  
  public SqlConnectionService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public NpgsqlConnection GetConnection() => new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString"));
}