using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL.Repositories;

public class TourLogRepository : ITourLogRepository
{
    private readonly TourPlannerContext _context;

    public TourLogRepository(TourPlannerContext context)
    {
        _context = context;
    }

    public List<TourLog> GetLogsByTourId(int tourId)
    {
        return _context.TourLogs.Where(tl => tl.TourId == tourId).ToList();
    }

    public TourLog? GetById(int id)
    {
        return _context.TourLogs.Find(id);
    }

    public TourLog Add(TourLog log)
    {
        _context.TourLogs.Add(log);
        _context.SaveChanges();
        return log;
    }

    public void Update(TourLog log)
    {
        _context.TourLogs.Update(log);
        _context.SaveChanges();
    }

    public void Delete(TourLog log)
    {
        _context.TourLogs.Remove(log);
        _context.SaveChanges();
    }
}