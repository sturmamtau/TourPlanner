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
    public ActionResult Create([FromBody] CreateTourDTO tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Add the new tour using the service
        GetTourDTO newTour = _tourService.AddTour(tour);
        
        //return new id and tour object (frontend should push this into tour list)
        return CreatedAtAction(nameof(GetById), new { id = newTour.Id }, newTour);  
    }
    [HttpGet]
    public ActionResult<List<Tour>> GetAll()
    {
        return Ok(_tourService.GetAllTours());
    }

    [HttpGet("{id}")]
    public ActionResult<GetTourDTO> GetById(int id)
    {
        var tour = _tourService.GetTourById(id);
        return Ok(tour);
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _tourService.DeleteTour(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] CreateTourDTO tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        GetTourDTO updatedTour = _tourService.UpdateTour(id, tour);
        return Ok(updatedTour);
    }
}
