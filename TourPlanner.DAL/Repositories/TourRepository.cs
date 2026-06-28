using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL.Repositories;

public class TourRepository : ITourRepository
{
    private readonly TourPlannerContext _context;

    public TourRepository(TourPlannerContext context)
    {
        _context = context;
    }

    public List<Tour> GetAllTours()
    {
        // Implementation here  
        return _context.Tours.ToList();
    }

    public Tour? GetTour(int id)
    {
        return _context.Tours.Find(id);
    }

    public void AddTour(Tour tour)
    {
        
    }

    public void DeleteTour(int id)
    {
        // Implementation here  
    }

    public void UpdateTour(Tour tour)
    {
        // Implementation here  
    }
}
