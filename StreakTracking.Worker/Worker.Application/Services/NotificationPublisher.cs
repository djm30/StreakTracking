using MassTransit;
using Microsoft.Extensions.Logging;
using StreakTracking.Events;
using StreakTracking.Events.Events;
using StreakTracking.Worker.Application.Contracts.Business;

namespace StreakTracking.Worker.Application.Services;

public class NotificationPublisher : INotificationPublisher
{
    
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<NotificationPublisher> _logger;

    public NotificationPublisher(IPublishEndpoint publishEndpoint, ILogger<NotificationPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }


    public async void Notify(NotificationType notificationType, string message, Guid? streakId)
    {

        if (!(notificationType != NotificationType.Information && streakId is null))
        {
            await _publishEndpoint.Publish(new NotificationEvent()
            {
                NotificationType = notificationType,
                Message = message,
                StreakId = streakId ?? null,
            });
            _logger.LogInformation("Notification Published");
        }
    }
}