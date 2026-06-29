using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.DAL.Repositories;
using TourPlanner.BL.DTOs;

namespace TourPlanner.BL;

public class TourService : ITourService
{
    private readonly ITourRepository _tourRepo;
    public TourService(ITourRepository tourRepo)
    {
        _tourRepo = tourRepo;
    }

    public List<GetTourDTO> GetAllTours()
    {
        var tours = _tourRepo.GetAllTours();

        return tours.Select(tour => new GetTourDTO  
        {
            Id = tour.Id,
            Name = tour.Name,
            Description = tour.Description,
            From = tour.From,
            To = tour.To,
            TransportType = tour.TransportType.ToString(),
            TourDistance = tour.TourDistance,
            EstimatedTime = tour.EstimatedTime,
            ImageUrl = tour.ImagePath != null
            ? $"/images/{Path.GetFileName(tour.ImagePath)}"
            : null
        }).ToList();
    }

    public GetTourDTO AddTour(CreateTourDTO tourDTO)
    {
        var tour = new Tour
        {
            Name = tourDTO.Name,
            Description = tourDTO.Description,
            From = tourDTO.From,
            To = tourDTO.To,
            TransportType = Enum.Parse<TransportType>(tourDTO.TransportType),
            TourDistance = 0, // This will be calculated later
            EstimatedTime = 0, // This will be calculated later
            ImagePath = "placeholder", // This will be set later
            UserId = 1 // This should be set to the currently logged-in user's ID
        };
        var addedTour = _tourRepo.AddTour(tour);
        return MapToGetTourDTO(addedTour);
    }
    public void DeleteTour(int id)
    {
        var tour = _tourRepo.GetTour(id);
        if (tour != null)
        {
            _tourRepo.DeleteTour(tour);
        }
        else
        {
            throw new KeyNotFoundException($"Tour with id {id} not found");
        }
    }

    public GetTourDTO UpdateTour(int id, CreateTourDTO updatedtour)
    {
        var tourToUpdate = _tourRepo.GetTour(id);
        if (tourToUpdate != null)
        {
            tourToUpdate.Name = updatedtour.Name;
            tourToUpdate.Description = updatedtour.Description;
            tourToUpdate.From = updatedtour.From;
            tourToUpdate.To = updatedtour.To;
            tourToUpdate.TransportType = Enum.Parse<TransportType>(updatedtour.TransportType);
            tourToUpdate.TourDistance = 0; // This will be calculated later
            tourToUpdate.EstimatedTime = 0; // This will be calculated later
            tourToUpdate.ImagePath = "placeholder"; // This will be set later
            tourToUpdate.UserId = 1; // This should be set to the currently logged-in user's ID - not really necessary here
            _tourRepo.UpdateTour(tourToUpdate);
            return MapToGetTourDTO(tourToUpdate);
        }
        else
        {
            throw new KeyNotFoundException($"Tour with id {id} not found");
        }

    }

    public GetTourDTO GetTourById(int id)
    {
        var tour = _tourRepo.GetTour(id);
        if (tour == null)
        {
            Console.WriteLine("Tour not found");
            throw new KeyNotFoundException($"Tour with id {id} not found");
        }
        Console.WriteLine("Tour found");
        return MapToGetTourDTO(tour);
    }

    private GetTourDTO MapToGetTourDTO(Tour tour)
    {
        return new GetTourDTO
        {
            Id = tour.Id,
            Name = tour.Name,
            Description = tour.Description,
            From = tour.From,
            To = tour.To,
            TransportType = tour.TransportType.ToString(),
            TourDistance = tour.TourDistance,
            EstimatedTime = tour.EstimatedTime,
            ImageUrl = tour.ImagePath != null
                ? $"/images/{Path.GetFileName(tour.ImagePath)}"
                : null
        };
    }

}
