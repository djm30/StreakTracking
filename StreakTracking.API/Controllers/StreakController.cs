using System.Net;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using StreakTracking.API.Services;
using StreakTracking.API.Models;
using StreakTracking.Events.Events;

namespace StreakTracking.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StreakController : ControllerBase
{
    private readonly ILogger<StreakController> _logger;
    private readonly IStreakReadingService _streakReadService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public StreakController(ILogger<StreakController> logger, IStreakReadingService streakReadService, IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _streakReadService = streakReadService ?? throw new ArgumentNullException(nameof(streakReadService));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #region BasicStreakCrud

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Streak>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Streak>> GetStreaks()
    {
        _logger.LogInformation("Recieved request for GetStreaks");
        var responseMessage = await _streakReadService.GetStreaks();
        if (responseMessage.StatusCode == HttpStatusCode.OK)
            return Ok(responseMessage.Content);
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetStreak")]
    [ProducesResponseType(typeof(Streak), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Streak>> GetStreakById(string id)
    {
        _logger.LogInformation("Recieved request for GetStreaksById");
        var responseMessage = await _streakReadService.GetStreakById(id);
        if (responseMessage.StatusCode == HttpStatusCode.OK)
            return Ok(responseMessage.Content);
        return NotFound(responseMessage.Content);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateStreak([FromBody] AddStreakEvent streakEvent)
    {
        _logger.LogInformation("Recieved request for CreateStreak, publishing {event} to RabbitMQ", streakEvent);
        await _publishEndpoint.Publish<AddStreakEvent>(streakEvent);
        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateStreak(string id, [FromBody] UpdateStreak updateInfo)
    {
        updateInfo.StreakId = Guid.Parse(id);
        var streakEvent = _mapper.Map<UpdateStreakEvent>(updateInfo);
        _logger.LogInformation("Recieved request for UpdateStreak, publishing {event} to RabbitMQ", streakEvent);
        await _publishEndpoint.Publish<UpdateStreakEvent>(streakEvent);
        return Ok();
    }

    [HttpDelete("{id}", Name = "DeleteStreak")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteStreak(string id)
    {
        var streakEvent = new DeleteStreakEvent { StreakId = Guid.Parse(id) };
        _logger.LogInformation("Recieved request for DeleteStreak, publishing {event} to RabbitMQ", streakEvent);
        await _publishEndpoint.Publish<DeleteStreakEvent>(streakEvent);
        return Ok();
    }


    #endregion


    [HttpGet("{id}/[action]", Name = "GetCurrentStreak")]
    [ActionName("Current")]
    [ProducesResponseType(typeof(CurrentStreak), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CurrentStreak>> GetCurrentStreak(string id)
    {
        _logger.LogInformation("Recieved request for GetCurrent");
        var responseMessage = await  _streakReadService.GetCurrentStreak(id);
        if (responseMessage.StatusCode == HttpStatusCode.OK)
            return Ok(responseMessage.Content);
        return NotFound(responseMessage.Content);
    }


    [HttpPost("{id}/[action]")]
    [ActionName("Complete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CompleteStreak(string id, [FromBody] StreakComplete streakCompleteInfo)
    {
        streakCompleteInfo.StreakId = Guid.Parse(id);
        var streakEvent = _mapper.Map<StreakCompleteEvent>(streakCompleteInfo);
        _logger.LogInformation("Recieved request for CompleteStreak, publishing {event} to RabbitMQ", streakEvent);
        await _publishEndpoint.Publish<StreakCompleteEvent>(streakEvent);
        return Ok();
    }

    // TODO this stuff here
    // Create a class library for infrastructure e.g repos and services

    [HttpGet("[action]")]
    [ActionName("Full")]
    [ProducesResponseType(typeof(IEnumerable<StreakInfo>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<StreakInfo>>> GetAllStreakStreakInfo(string id)
    {
        return Ok();
    }

    [HttpGet("{id}/[action]")]
    [ActionName("Full")]
    [ProducesResponseType(typeof(StreakInfo), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StreakInfo>> GetFullStreakInfo(string id)
    {
        return Ok();
    }


}