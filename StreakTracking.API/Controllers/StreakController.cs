using System.Net;
using Microsoft.AspNetCore.Mvc;
using StreakTracking.API.Models;
using StreakTracking.Application.Contracts.Business;
using StreakTracking.Application.Models;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.API.Controllers;

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
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Streak>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Streak>>> GetStreaks()
    {
        _logger.LogInformation("Received request for GetStreaks");
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
        _logger.LogInformation("Received request for GetStreaksById");
        var responseMessage = await _streakReadService.GetStreakById(id);
        return responseMessage.StatusCode == HttpStatusCode.OK
            ? Ok(responseMessage.Content)
            : NotFound(responseMessage);
    }
    
    [HttpGet("{id}/[action]", Name = "GetCurrentStreak")]
    [ActionName("Current")]
    [ProducesResponseType(typeof(CurrentStreak), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CurrentStreak>> GetCurrentStreak(string id)
    {
        _logger.LogInformation("Received request for GetCurrent");
        var responseMessage = await  _streakReadService.GetCurrentStreak(id);
        return responseMessage.StatusCode == HttpStatusCode.OK
            ? Ok(responseMessage.Content)
            : NotFound(responseMessage);
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

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> CreateStreak([FromBody] AddStreakDTO addStreakDTO)
    {
        var responseMessage = await _publishingService.PublishCreateStreak(addStreakDTO);
        return responseMessage.StatusCode switch
        {
            HttpStatusCode.Accepted => Accepted(responseMessage),
            HttpStatusCode.BadRequest => BadRequest(responseMessage),
            _ => StatusCode((int)HttpStatusCode.InternalServerError)
        };
    }
    
    [HttpPost("{id}/[action]")]
    [ActionName("Complete")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> CompleteStreak(string id, [FromBody] StreakCompleteDTO streakCompleteDTO)
    {
        var responseMessage = await _publishingService.PublishStreakComplete(id, streakCompleteDTO);
        return responseMessage.StatusCode switch
        {
            HttpStatusCode.Accepted => Accepted(responseMessage),
            HttpStatusCode.BadRequest => BadRequest(responseMessage),
            _ => StatusCode((int)HttpStatusCode.InternalServerError)
        };
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> UpdateStreak(string id, [FromBody] UpdateStreakDTO updateStreakDTO)
    {
        var responseMessage = await _publishingService.PublishUpdateStreak(id, updateStreakDTO);
        return responseMessage.StatusCode switch
        {
            HttpStatusCode.Accepted => Accepted(responseMessage),
            HttpStatusCode.BadRequest => BadRequest(responseMessage),
            _ => StatusCode((int)HttpStatusCode.InternalServerError)
        };
    }

    [HttpDelete("{id}", Name = "DeleteStreak")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> DeleteStreak(string id)
    {
        var responseMessage = await _publishingService.PublishDeleteStreak(id);
        return responseMessage.StatusCode switch
        {
            HttpStatusCode.Accepted => Accepted(responseMessage),
            HttpStatusCode.BadRequest => BadRequest(responseMessage),
            _ => StatusCode((int)HttpStatusCode.InternalServerError)
        };
    }
}