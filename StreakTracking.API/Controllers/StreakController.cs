using System.Net;
using Microsoft.AspNetCore.Mvc;
using StreakTracking.API.Services;
using StreakTracking.API.Models;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StreakController : ControllerBase
{
    private readonly ILogger<StreakController> _logger;
    private readonly IStreakReadingService _streakReadService;
    private readonly IEventPublishingService _publishingService;

    public StreakController(ILogger<StreakController> logger, IStreakReadingService streakReadService, IEventPublishingService publishingService)
    {
        _logger = logger;
        _streakReadService = streakReadService;
        _publishingService = publishingService;
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
    public async Task<ActionResult> CreateStreak([FromBody] AddStreakDTO addStreakDTO)
    {
        var responseMessage = await _publishingService.PublishCreateStreak(addStreakDTO);
        if (responseMessage.StatusCode == HttpStatusCode.Accepted)
            return Accepted(responseMessage.Message);
        return Problem();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateStreak(string id, [FromBody] UpdateStreakDTO updateStreakDTO)
    {
        var responseMessage = await _publishingService.PublishUpdateStreak(id, updateStreakDTO);
        if (responseMessage.StatusCode == HttpStatusCode.Accepted)
            return Accepted(responseMessage.Message);
        return Problem();
    }

    [HttpDelete("{id}", Name = "DeleteStreak")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteStreak(string id)
    {
        var responseMessage = await _publishingService.PublishDeleteStreak(id);
        if (responseMessage.StatusCode == HttpStatusCode.Accepted)
            return Accepted(responseMessage.Message);
        return Problem();
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
    public async Task<ActionResult> CompleteStreak(string id, [FromBody] StreakCompleteDTO streakCompleteDTO)
    {
        var responseMessage = await _publishingService.PublishStreakComplete(id, streakCompleteDTO);
        if (responseMessage.StatusCode == HttpStatusCode.Accepted)
            return Accepted(responseMessage.Message);
        return Problem();
    }

    // TODO this stuff here

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