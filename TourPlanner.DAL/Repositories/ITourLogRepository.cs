using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL.Repositories
{
    public interface ITourLogRepository
    {
        List<TourLog> GetLogsByTourId(int tourId);
        TourLog? GetById(int id);
        TourLog Add(TourLog log);
        void Update(TourLog log);
        void Delete(TourLog log);
    }
}
