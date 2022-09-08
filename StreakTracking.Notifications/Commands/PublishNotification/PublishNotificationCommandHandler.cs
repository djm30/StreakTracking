using MediatR;
using StreakTracking.Events;
using StreakTracking.Notifications.Models;
using StreakTracking.Notifications.Services;

namespace StreakTracking.Notifications.Commands.PublishNotification;

public class PublishNotificationCommandHandler : IRequestHandler<PublishNotificationCommand, Unit>
{
    private readonly INotificationService _notificationService;
    
    public PublishNotificationCommandHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(PublishNotificationCommand request, CancellationToken cancellationToken)
    {
        BaseNotification? notification = request.NotificationType switch
        {
            NotificationType.Information => new BaseNotification() { Message = request.Message },
            NotificationType.Refetch => new RefetchNotification()
            {
                Message = request.Message, StreakId = request.StreakId!
            },
            NotificationType.Delete => new DeleteNotification()
            {
                Message = request.Message, StreakId = request.StreakId!
            },
            _ => null
        };

        if(notification is not null)  await _notificationService.Notify(request.NotificationType, notification);
        return Unit.Value;
    }
}