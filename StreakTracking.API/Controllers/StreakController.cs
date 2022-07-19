using System.Net;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using StreakTracking.Streaks.Models;
using StreakTracking.Events.Events;

namespace StreakTracking.Streaks.Controllers;

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

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Streak>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Streak>> GetStreaks()
    {
        var streaks = await _repository.GetStreaks();
        return Ok(streaks);
    }

    [HttpGet("{id}", Name = "GetStreak")]
    [ProducesResponseType(typeof(Streak), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Streak>> GetStreakById(string id)
    {
        var streak = await _repository.GetStreakById(id);
        return Ok(streak);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateStreak([FromBody] AddStreakEvent streakEvent)
    {
        await _publishEndpoint.Publish<AddStreakEvent>(streakEvent);
        return Ok();
    }

    [HttpPost("[action]")]
    [ActionName("Complete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> CompleteStreak([FromBody] StreakCompleteEvent streakEvent)
    {
        await _publishEndpoint.Publish<StreakCompleteEvent>(streakEvent);
        return Ok();
    }
}