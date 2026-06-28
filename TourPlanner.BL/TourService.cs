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

    public List<TourDTO> GetAllTours()
    {
        var tours = _tourRepo.GetAllTours();

        return tours.Select(tour => new TourDTO  
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

    public TourDTO AddTour (TourDTO tour)
    {

    }

    public void DeleteTour(int id)
    {
        throw new NotImplementedException();
    }

    public TourDTO UpdateTour(TourDTO tour)
    {
        throw new NotImplementedException();
    }

    public TourDTO GetTourById(int id)
    {
        var tour = _tourRepo.GetTour(id);
        if (tour == null)
        {
            throw new Exception($"Tour with id {id} not found");
        }
        return new TourDTO
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
