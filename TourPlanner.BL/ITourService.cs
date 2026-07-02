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
    List<GetTourDTO> GetAllTours();
    GetTourDTO GetTourById(int id);
    Task<GetTourDTO> UpdateTour(int id, CreateTourDTO tour);
    GetTourDTO UpdateTourImage(int id, string imagePath);
    Task<GetTourDTO> AddTour(CreateTourDTO tour); 
    void DeleteTour(int id);
}
