using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.Models;
using TourPlanner.BL;
using Microsoft.EntityFrameworkCore;
using TourPlanner.DAL.Repositories;
using TourPlanner.BL.DTOs;
using Microsoft.AspNetCore.Hosting;

namespace TourPlanner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToursController : ControllerBase
{
    private readonly ITourService _tourService;
    private readonly IWebHostEnvironment _environment;

    public ToursController(ITourService tourService, IWebHostEnvironment environment)
    {
        //_context = context;
        _tourService = tourService;
        _environment = environment;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateTourDTO tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Add the new tour using the service
        GetTourDTO newTour = await _tourService.AddTour(tour);
        
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
        return Ok(_tourService.GetTourById(id));
    }

    [HttpPost("{id}/image")]
    public async Task<ActionResult<GetTourDTO>> UploadImage(int id, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No image uploaded.");
        }

        try
        {
            var rootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsFolder = Path.Combine(rootPath, "images");

            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"tour_{id}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = $"/images/{fileName}";
            var updatedTour = _tourService.UpdateTourImage(id, relativePath);

            return Ok(updatedTour);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern des Bildes: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _tourService.DeleteTour(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreateTourDTO tour)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        GetTourDTO updatedTour = await _tourService.UpdateTour(id, tour);
        return Ok(updatedTour);
    }
}
