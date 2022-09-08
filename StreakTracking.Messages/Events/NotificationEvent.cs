namespace StreakTracking.Events.Events;

public class NotificationEvent
{
    public NotificationType NotificationType { get; set; }
    public string Message { get; set; }
    public Guid? StreakId { get; set; }
}