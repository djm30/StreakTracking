using System.Text.Json.Serialization;

namespace StreakTracking.Application.Models;

public class UpdateStreakDTO
{
    [JsonIgnore]
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
    public int LongestStreak { get; set; }
}