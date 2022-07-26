using System.Net;
using AutoMapper;
using MassTransit;
using StreakTracking.API.Models;
using StreakTracking.Events.Events;

namespace StreakTracking.API.Services;

public class EventPublishingService : IEventPublishingService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public EventPublishingService(IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<ResponseMessage> PublishCreateStreak(AddStreakDTO addStreakDTO)
    {
        addStreakDTO.StreakId = new Guid();
        var addStreakEvent = _mapper.Map<AddStreakEvent>(addStreakDTO);
        await _publishEndpoint.Publish<AddStreakEvent>(addStreakEvent);
        return new ResponseMessage()
        {
            Message = $"Creating new Streak with ID: {addStreakEvent.StreakId}", 
            StatusCode = HttpStatusCode.Accepted
        };
    }

    public async Task<ResponseMessage> PublishUpdateStreak(string StreakId, UpdateStreakDTO updateStreakDTO)
    {
        updateStreakDTO.StreakId = Guid.Parse(StreakId);
        var updateStreakEvent = _mapper.Map<UpdateStreakEvent>(updateStreakDTO);
        await _publishEndpoint.Publish<UpdateStreakEvent>(updateStreakEvent);
        return new ResponseMessage()
        {
            Message = $"Currently updating Streak with ID: {updateStreakEvent.StreakId}", 
            StatusCode = HttpStatusCode.Accepted
        };
    }

    public async Task<ResponseMessage> PublishDeleteStreak(string StreakId)
    {
        var deleteStreakEvent = new DeleteStreakEvent { StreakId = Guid.Parse(StreakId) };
        await _publishEndpoint.Publish<DeleteStreakEvent>(deleteStreakEvent);
        return new ResponseMessage()
        {
            Message = $"Currently deleting Streak with ID: {deleteStreakEvent.StreakId}",
            StatusCode = HttpStatusCode.Accepted
        };
    }

    public async Task<ResponseMessage> PublishStreakComplete(string StreakId, StreakCompleteDTO streakCompleteDTO)
    {
        streakCompleteDTO.StreakId = Guid.Parse(StreakId);
        var streakCompleteEvent = _mapper.Map<StreakCompleteEvent>(streakCompleteDTO);
        await _publishEndpoint.Publish<StreakCompleteEvent>(streakCompleteEvent);
        return new ResponseMessage()
        {
            Message =
                $"Currently marking Streak with ID: {streakCompleteEvent.StreakId} complete for day: {streakCompleteEvent.Date.Date}",
            StatusCode = HttpStatusCode.Accepted
        };
    }
}