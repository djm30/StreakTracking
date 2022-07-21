using System.Text.Json.Serialization;

namespace StreakTracking.Models;

public class UpdateStreak
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
    public int LongestStreak { get; set; }
}