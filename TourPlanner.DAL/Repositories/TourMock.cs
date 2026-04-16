using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL.Repositories;

public class TourMock : ITourRepository
{
    private static readonly List<Tour> Tours = new List<Tour>();
    public TourMock()
    {
        Tour Tour1 = new Tour
        {
            Id = 1,
            Name = "Tour1",
            Description = "Description1",
            From = "From1",
            To = "To1",
            TransportType = TransportType.Vacation,
            TourDistance = 100,
            EstimatedTime = 120,
            ImagePath = "path/to/image1.jpg",
            UserId = 1
        };

        Tour Tour2 = new Tour
        {
            Id = 2,
            Name = "Tour2",
            Description = "Description2",
            From = "From2",
            To = "To2",
            TransportType = TransportType.Bike,
            TourDistance = 50,
            EstimatedTime = 60,
            ImagePath = "path/to/image2.jpg",
            UserId = 1
        };

        Tour Tour3 = new Tour
        {
            Id = 3,
            Name = "Tour3",
            Description = "Description3",
            From = "From3",
            To = "To3",
            TransportType = TransportType.Walk,
            TourDistance = 10,
            EstimatedTime = 30,
            ImagePath = "path/to/image3.jpg",
            UserId = 1
        };

        if (!Tours.Any())
        {
            Tours.Add(Tour1);
            Tours.Add(Tour2);
            Tours.Add(Tour3);
        }
    }
    public List<Tour> GetAllTours()
    {
        return Tours;
    }
    //Tour GetTourById(int id);
    public void AddTour(Tour tour)
    {
        int nextId = Tours.Any() ? Tours.Max(t => t.Id) + 1 : 1;
    
        tour.Id = nextId;
    
        // Falls die Tour-Logs Liste in der neuen Tour null ist, initialisieren
        if (tour.TourLogs == null) {
            tour.TourLogs = new List<TourLog>();
        }

        Tours.Add(tour);
        Console.WriteLine($"[Mock] Neue Tour erstellt: {tour.Name} mit ID {tour.Id}");
    }

    public void DeleteTour(int id)
    {
        var tourToDelete = Tours.FirstOrDefault(t => t.Id == id);
        if (tourToDelete != null)
        {
            Tours.Remove(tourToDelete);
        }
        Console.WriteLine($"{Tours.Count}");
    }
    public void UpdateTour(Tour tour)
    {
        var TourToUpdate = Tours.FirstOrDefault(t => t.Id == tour.Id);
        if (TourToUpdate != null)
        {
            TourToUpdate.Name = tour.Name;
            TourToUpdate.Description = tour.Description;
            TourToUpdate.From = tour.From;
            TourToUpdate.To = tour.To;
            TourToUpdate.TransportType = tour.TransportType;
            TourToUpdate.TourDistance = tour.TourDistance;
            TourToUpdate.EstimatedTime = tour.EstimatedTime;
            TourToUpdate.ImagePath = tour.ImagePath;
            TourToUpdate.UserId = tour.UserId;
        }
    }
}
