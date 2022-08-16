using System;
using System.Threading.Tasks;
using MassTransit;
using StreakTracking.Events.Events;
using StreakTracking.Worker.Application.Contracts.Business;

namespace StreakTracking.Worker.EventHandler.Consumers;

public class StreakCompleteConsumer : IConsumer<StreakCompleteEvent>
{
    private readonly IStreakWriteService _service;

    public StreakCompleteConsumer(IStreakWriteService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task Consume(ConsumeContext<StreakCompleteEvent> context)
    {
        await _service.MarkComplete(context.Message);
    }
}
