using System.Text.Json.Serialization;

namespace StreakTracking.Events.Events;

public class UpdateStreakEvent
{
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
    public int LongestStreak { get; set; }
}