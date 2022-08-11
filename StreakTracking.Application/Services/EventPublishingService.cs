using AutoMapper;
using MassTransit;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Streaks.Commands.AddStreak;
using StreakTracking.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Application.Streaks.Commands.UpdateStreak;
using StreakTracking.Events.Events;

namespace StreakTracking.Application.Services;

public class EventPublishingService : IEventPublishingService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    
    public EventPublishingService(IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    
    // TODO RECONFIGURE MAPPING PROFILES

    public async Task PublishCreateStreak(AddStreakCommand addStreakCommand)
    {

        var addStreakEvent = _mapper.Map<AddStreakEvent>(addStreakCommand);
        await _publishEndpoint.Publish<AddStreakEvent>(addStreakEvent);

    }

    public async Task PublishUpdateStreak(UpdateStreakCommand updateStreakCommand)
    {
        var updateStreakEvent = _mapper.Map<UpdateStreakEvent>(updateStreakCommand);
        await _publishEndpoint.Publish<UpdateStreakEvent>(updateStreakEvent);
    }

    public async Task PublishDeleteStreak(DeleteStreakCommand deleteStreakCommand)
    {
        var deleteStreakEvent = _mapper.Map<StreakCompleteEvent>(deleteStreakCommand);
        await _publishEndpoint.Publish<DeleteStreakEvent>(deleteStreakEvent);

    }

    public async Task PublishStreakComplete(StreakCompleteCommand streakCompleteCommand)
    {
        var streakCompleteEvent = _mapper.Map<StreakCompleteEvent>(streakCompleteCommand);
        await _publishEndpoint.Publish<StreakCompleteEvent>(streakCompleteEvent);
    }
}