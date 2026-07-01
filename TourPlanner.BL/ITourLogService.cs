using System.Collections.Generic;
using TourPlanner.BL.DTOs;

namespace TourPlanner.BL;

public interface ITourLogService
{
    List<TourLogDTO> GetLogsForTour(int tourId);
    TourLogDTO AddTourLog(CreateTourLogDTO dto);
    void UpdateTourLog(int id, CreateTourLogDTO dto);
    void DeleteTourLog(int id);
}