using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.Models;
using TourPlanner.BL;
using Microsoft.EntityFrameworkCore;
using TourPlanner.DAL.Repositories;
using TourPlanner.BL.DTOs;

namespace TourPlanner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToursController : ControllerBase
{
    //private readonly TourPlannerContext _context;
    private readonly ITourService _tourService;

    public ToursController(ITourService tourService)
    {
        //_context = context;
        _tourService = tourService;
    }

    // Speichert eine neue Tour, die aus dem Angular-Formular kommt
    
    [HttpPost]
    public ActionResult Create([FromBody] Tour tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _tourService.AddTour(tour);
        return Created();
    }
    [HttpGet]
    public ActionResult<List<Tour>> GetAll()
    {
        return Ok(_tourService.GetAllTours());
    }

    [HttpGet("{id}")]
    public ActionResult<TourDTO> GetById(int id)
    {
        var tour = _tourService.GetTourById(id);
        return Ok(tour);
    }
    /*
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _tourService.DeleteTour(id);
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
        _tourService.UpdateTour(tour);
        return Ok();
    }
    */
}
