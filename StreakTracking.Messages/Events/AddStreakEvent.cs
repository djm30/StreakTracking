namespace StreakTracking.Events.Events;

public class AddStreakEvent
{
    public Guid StreakId { get; set; }
    public string StreakName { get; set; }
    public string StreakDescription { get; set; }
}