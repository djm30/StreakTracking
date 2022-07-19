namespace StreakTracking.Events.Events;

public class StreakCompleteEvent
{
    public string Id { get; set; }
    public bool Complete { get; set; }
    public DateOnly Date { get; set; }
}