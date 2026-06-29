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

    public Tour AddTour(Tour tour)
    {
        // prepare changes
        _context.Tours.Add(tour);
        // update database
        _context.SaveChanges();
        // Id is added automatically (returning id and tour object is updated)
        return tour;
    }

    public void DeleteTour(Tour tour)
    {
        _context.Tours.Remove(tour);
        _context.SaveChanges();
    }

    public void UpdateTour(Tour tour)
    {
        _context.Tours.Update(tour);
        _context.SaveChanges();
    }
}
