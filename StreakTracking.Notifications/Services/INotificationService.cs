using StreakTracking.Events;
using StreakTracking.Notifications.Models;

namespace StreakTracking.Notifications.Services;

public interface INotificationService
{
    public Task Notify<TNotification>(NotificationType type, TNotification? notification) where TNotification : BaseNotification;

}