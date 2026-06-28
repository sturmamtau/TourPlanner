using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.BL.DTOs;

namespace TourPlanner.BL;

public interface ITourService
{
    //evtl andere DTOS nötig
    List<TourDTO> GetAllTours();
    TourDTO GetTourById(int id);
    TourDTO UpdateTour(TourDTO tour);
    TourDTO AddTour(TourDTO tour); 
    void DeleteTour(int id);
}
