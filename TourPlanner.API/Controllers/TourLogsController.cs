using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TourPlanner.BL;
using TourPlanner.BL.DTOs;

namespace TourPlanner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TourLogsController : ControllerBase
{
    private readonly ITourLogService _logService;

    public TourLogsController(ITourLogService logService) 
    { 
        _logService = logService;
    }
    
    [HttpGet("tour/{tourId}")]
    public ActionResult<List<TourLogDTO>> GetLogsForTour(int tourId)
    {
        return Ok(_logService.GetLogsForTour(tourId));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTourLogDTO tourLogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdLog = _logService.AddTourLog(tourLogDto);
        return Created("", createdLog);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CreateTourLogDTO tourLogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            _logService.UpdateTourLog(id, tourLogDto);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            _logService.DeleteTourLog(id);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}