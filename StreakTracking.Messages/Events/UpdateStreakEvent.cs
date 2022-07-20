namespace StreakTracking.Events.Events;

public class UpdateStreakEvent
{
    public Guid StreakId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int LongestStreak { get; set; }
}