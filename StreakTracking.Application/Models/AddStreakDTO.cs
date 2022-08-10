using System.Text.Json.Serialization;

namespace StreakTracking.Application.Models;

public class AddStreakDTO
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
}