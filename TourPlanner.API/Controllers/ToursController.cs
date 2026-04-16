using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.DAL;
using TourPlanner.Models;
using Microsoft.EntityFrameworkCore;
using TourPlanner.DAL.Repositories;

namespace TourPlanner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToursController : ControllerBase
{
    //private readonly TourPlannerContext _context;
    private readonly TourMock _tourMock;
    private readonly TourLogMock _tourLogMock;

    public ToursController(TourPlannerContext context, TourMock tourMock, TourLogMock tourLogMock)
    {
        //_context = context;
        _tourMock = tourMock;
        _tourLogMock = tourLogMock;
    }

    // Speichert eine neue Tour, die aus dem Angular-Formular kommt
    [HttpPost]
    public ActionResult Create([FromBody] Tour tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _tourMock.AddTour(tour);
        return Created();
    }

    [HttpGet]
    public ActionResult<List<Tour>> GetAll()
    {
        var tours = _tourMock.GetAllTours();
        var logs = _tourLogMock.GetAllTourLogs();

        // Jede Tour bekommt ihre passenden Logs zugewiesen
        foreach (var tour in tours)
        {
            tour.TourLogs = logs.Where(l => l.TourId == tour.Id).ToList();
            Console.WriteLine($"Tour {tour.Id} hat {tour.TourLogs.Count} Logs");
        }
        
        return Ok(tours);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _tourMock.DeleteTour(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] Tour tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id != tour.Id)
        {
            return BadRequest();
        }
        _tourMock.UpdateTour(tour);
        return Ok();
    }
}
