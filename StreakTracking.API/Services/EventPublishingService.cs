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
        if (addStreakDTO.Description == "" || addStreakDTO.Name == "")
        {
            return new ResponseMessage()
            {
                Message = "Validation failed: Please provide both a name and a description",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
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
        if (updateStreakDTO.LongestStreak < 0 || updateStreakDTO.StreakDescription == "" ||
            updateStreakDTO.StreakName == "")
        {
            return new ResponseMessage()
            {
                Message = "Validation failed: Please provide a name, description and a valid value for the longest streak",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
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
        if (streakCompleteDTO.Date > DateTime.Today)
        {
            return new ResponseMessage()
            {
                Message = "Provided date cannot be in the future",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
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