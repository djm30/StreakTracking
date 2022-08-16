using Microsoft.AspNetCore.Mvc;
using StreakTracking.Endpoints.Application.Helpers;
using StreakTracking.Endpoints.Application.Models;

namespace StreakTracking.Endpoints.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ISeedDatabase _seeder;

    public DatabaseController(ISeedDatabase seeder)
    {
        _seeder = seeder;
    }


    [HttpPost("[action]")]
    [ActionName("Create")]
    public async Task<ActionResult<ResponseMessage>> CreateTables()
    {
        try
        {
            await _seeder.CreateTables();
            return Ok(new ResponseMessage() { Message = "Tables Created" });
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage() { Message = $"Error creating tables: {e}" });
        }
    }
    
    [HttpPost("[action]")]
    [ActionName("Seed")]
    public async Task<ActionResult<ResponseMessage>> SeedDatabase()
    {
        try
        {
            await _seeder.SeedStreaks();
            return Ok(new ResponseMessage() { Message = "Streaks Created" });
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage() { Message = $"Error creating tables: {e}" });
        }
    }

    [HttpPost("[action]")] [ActionName("Drop")]
    public async Task<ActionResult<ResponseMessage>> DropTables()
    {
        try
        {
            await _seeder.ClearDatabase();
            return Ok(new ResponseMessage() { Message = "Tables dropped" });
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage() { Message = $"Error creating streaks: {e}" });
        }
    }
}