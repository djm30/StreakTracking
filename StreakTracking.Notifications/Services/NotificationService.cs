using Microsoft.AspNetCore.SignalR;
using StreakTracking.Events;
using StreakTracking.Notifications.Hubs;
using StreakTracking.Notifications.Models;

namespace StreakTracking.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IHubContext<NotificationHub> hubContext, ILogger<NotificationService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }


    public async Task Notify<TNotification>(NotificationType type, TNotification notification) where TNotification : BaseNotification
    {
        _logger.LogInformation("Type: {0}, Body: {1}", type, notification);
        await _hubContext.Clients.All.SendAsync(type.ToString(), arg1: notification);
    }
}