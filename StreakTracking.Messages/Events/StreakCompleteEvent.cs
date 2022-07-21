using System.Runtime.Serialization;

namespace StreakTracking.Events.Events;

public class StreakCompleteEvent
{
    public Guid StreakId { get; set; }
    public bool Complete { get; set; }
    public DateTime Date { get; set; }
}