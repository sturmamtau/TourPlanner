using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL.Repositories;

public interface ITourRepository
{
    List<Tour> GetAllTours();
    //Tour GetTourById(int id);
    public Tour AddTour(Tour tour);

    public void DeleteTour(Tour id); 

    public void UpdateTour(Tour tour);

    public Tour? GetTour(int id);
}
