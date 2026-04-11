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

    public ToursController(TourPlannerContext context)
    {
        //_context = context;
        _tourMock = new TourMock();
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
        return Ok(_tourMock.GetAllTours());
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
