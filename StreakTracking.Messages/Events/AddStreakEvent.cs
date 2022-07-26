namespace StreakTracking.Events.Events;

public class AddStreakEvent
{
    public Guid StreakId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}