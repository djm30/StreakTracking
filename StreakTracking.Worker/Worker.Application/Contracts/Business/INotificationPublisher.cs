using StreakTracking.Events;
using StreakTracking.Events.Events;

namespace StreakTracking.Worker.Application.Contracts.Business;

public interface INotificationPublisher
{
    public void Notify(NotificationType notificationType, string message, Guid? streakId = null);
}