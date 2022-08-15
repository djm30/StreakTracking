using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StreakTracking.Application.Models;
using StreakTracking.Application.Streaks.Commands.AddStreak;
using StreakTracking.Application.Streaks.Commands.DeleteStreak;
using StreakTracking.Application.Streaks.Commands.StreakComplete;
using StreakTracking.Application.Streaks.Commands.UpdateStreak;
using StreakTracking.Application.Streaks.Queries.GetCurrentStreak;
using StreakTracking.Application.Streaks.Queries.GetFullStreakInfoById;
using StreakTracking.Application.Streaks.Queries.GetFullStreaksInfo;
using StreakTracking.Application.Streaks.Queries.GetStreakByID;
using StreakTracking.Application.Streaks.Queries.GetStreaks;
using StreakTracking.Domain.Calculated;
using StreakTracking.Domain.Entities;

namespace StreakTracking.API.Controllers;


[ApiController]
[Route("/api/v1/[controller]")]
public class StreaksController : ControllerBase
{
    private readonly ILogger<StreaksController> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StreaksController(ILogger<StreaksController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Streak>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Streak>>> GetStreaks()
    {
        _logger.LogInformation("Received request for GetStreaks");
        var response = await _mediator.Send(new GetStreaksQuery());
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}", Name = "GetStreak")]
    [ProducesResponseType(typeof(Streak), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Streak>> GetStreakById(string id)
    {
        _logger.LogInformation("Received request for GetStreaksById");
        var response = await _mediator.Send(new GetStreakByIdQuery() { StreakId = id });
        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpGet("[action]/{id}", Name = "GetCurrentStreak")]
    [ActionName("Current")]
    [ProducesResponseType(typeof(CurrentStreak), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<CurrentStreak>> GetCurrentStreak(string id)
    {
        _logger.LogInformation("Received request for GetCurrent");
        var response = await _mediator.Send(new GetCurrentStreakQuery() { StreakId = id });
        return StatusCode((int)response.StatusCode, response);
    }
    
    // TODO this stuff here

    [HttpGet("[action]")]
    [ActionName("Full")]
    [ProducesResponseType(typeof(IEnumerable<FullStreakInfo>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<FullStreakInfo>>> GetAllFullStreakInfo()
    {
        _logger.LogInformation("Received request for Full Streak");
        var response = await _mediator.Send(new GetFullStreaksInfoQuery());
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("[action]/{id}")]
    [ActionName("Full")]
    [ProducesResponseType(typeof(FullStreakInfo), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<FullStreakInfo>> GetFullStreakInfoByID(string id)
    {
        _logger.LogInformation("Received request for Full Streak Info for id: {id}", id);
        var response = await _mediator.Send(new GetFullStreakInfoByIdQuery(){StreakId = id});
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> CreateStreak([FromBody] AddStreakDTO addStreakDTO)
    {
        _logger.LogInformation("Received request for CreateStreak");
        var addStreakCommand = _mapper.Map<AddStreakCommand>(addStreakDTO);
        var response = await _mediator.Send(addStreakCommand);
        return StatusCode((int)response.StatusCode, response);
    }
    
    [HttpPost("[action]/{id}")]
    [ActionName("Complete")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> CompleteStreak(string id, [FromBody] StreakCompleteDTO streakCompleteDTO)
    {
        _logger.LogInformation("Received request for CompleteStreak for Id: {id}", id);
        var addStreakCommand = _mapper.Map<StreakCompleteCommand>(streakCompleteDTO);
        addStreakCommand.stringStreakId = id;
        var response = await _mediator.Send(addStreakCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> UpdateStreak(string id, [FromBody] UpdateStreakDTO updateStreakDTO)
    {
        _logger.LogInformation("Received request for UpdateStreak for Id: {id}", id);
        var updateStreakCommand = _mapper.Map<UpdateStreakCommand>(updateStreakDTO);
        updateStreakCommand.stringStreakId = id;
        var response = await _mediator.Send(updateStreakCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}", Name = "DeleteStreak")]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResponseMessage),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseMessage>> DeleteStreak(string id)
    {
        _logger.LogInformation("Received request for DeleteStreak for Id: {id}", id);
        var response = await _mediator.Send( new DeleteStreakCommand(){stringStreakId = id});
        return StatusCode((int)response.StatusCode, response);
    }
}