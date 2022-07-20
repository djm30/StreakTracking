namespace StreakTracking.Events.Events;

public class StreakCompleteEvent
{
    public Guid Id { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}