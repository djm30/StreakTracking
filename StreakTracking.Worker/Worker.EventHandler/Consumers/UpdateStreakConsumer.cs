using System;
using System.Threading.Tasks;
using MassTransit;
using StreakTracking.Events.Events;
using StreakTracking.Worker.Application.Contracts.Business;

namespace StreakTracking.Worker.EventHandler.Consumers;

public class UpdateStreakConsumer : IConsumer<UpdateStreakEvent>
{
    private readonly IStreakWriteService _service;

    public UpdateStreakConsumer(IStreakWriteService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    public async Task Consume(ConsumeContext<UpdateStreakEvent> context)
    {
        await _service.UpdateStreak(context.Message);
    }
}