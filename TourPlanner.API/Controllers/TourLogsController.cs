using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.DAL.Repositories;
using TourPlanner.Models;

namespace TourPlanner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TourLogsController : ControllerBase
{
    public TourLogMock _tourLogMock;
    public TourLogsController() { 
        _tourLogMock = new TourLogMock();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_tourLogMock.GetAllTourLogs());
    }

    [HttpPost]
    public IActionResult Create([FromBody] TourLog tourLog)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _tourLogMock.AddTourLog(tourLog);
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TourLog tourLog)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != tourLog.Id)
        {
            return BadRequest();
        }
        _tourLogMock.UpdateTourLog(tourLog);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _tourLogMock.DeleteTourLog(id);
        return Ok();
    }
}
