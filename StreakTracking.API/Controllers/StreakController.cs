using System.Net;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using StreakTracking.Events.Events;
using StreakTracking.Models;
using StreakTracking.Repositories;

namespace StreakTracking.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StreakController : ControllerBase
{
    private readonly ILogger<StreakController> _logger;
    private readonly IStreakReadRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public StreakController(ILogger<StreakController> logger, IStreakReadRepository repository, IPublishEndpoint publishEndpoint)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }
    
    #region BasicStreakCrud

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Streak>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Streak>> GetStreaks()
    {
        _logger.LogInformation("Recieved request for GetStreaks");
        var streaks = await _repository.GetStreaks();
        return Ok(streaks);
    }

    [HttpGet("{id}", Name = "GetStreak")]
    [ProducesResponseType(typeof(Streak), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Streak>> GetStreakById(string id)
    {
        _logger.LogInformation("Recieved request for GetStreaksById");
        var streak = await _repository.GetStreakById(id);
        return Ok(streak);
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
    
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateStreak([FromBody] UpdateStreakEvent streakEvent)
    {
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
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> GetCurrentStreak(string id)
    {
        _logger.LogInformation("Recieved request for GetCurrent");
        var current = await _repository.GetCurrent(id);
        return Ok(current);
    }

    
    [HttpPost("[action]")]
    [ActionName("Complete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CompleteStreak([FromBody] StreakCompleteEvent streakEvent)
    {
        _logger.LogInformation("Recieved request for CompleteStreak, publishing {event} to RabbitMQ", streakEvent);
        await _publishEndpoint.Publish<StreakCompleteEvent>(streakEvent);
        return Ok();
    }
}