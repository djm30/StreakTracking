using MediatR;
using StreakTracking.Events;

namespace StreakTracking.Notifications.Commands.PublishNotification;

public class PublishNotificationCommand : IRequest<Unit>
{
    public NotificationType NotificationType { get; set; }
    public string Message { get; set; }
    public string? StreakId { get; set; }
}