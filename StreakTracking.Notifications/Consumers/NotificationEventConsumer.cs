using MassTransit;
using MediatR;
using StreakTracking.Events.Events;
using StreakTracking.Notifications.Commands.PublishNotification;

namespace StreakTracking.Notifications.Consumers;

public class NotificationEventConsumer : IConsumer<NotificationEvent>
{
    private readonly IMediator _mediator;
    
    public NotificationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<NotificationEvent> context)
    {
        await _mediator.Send(new PublishNotificationCommand()
        {
            Message = context.Message.Message,
            StreakId = context.Message.StreakId?.ToString(),
            NotificationType = context.Message.NotificationType
        });
    }
}