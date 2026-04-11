using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DAL.Repositories;

public class TourLogMock : ITourLogRepository
{
    private static readonly List<TourLog> TourLogs = new List<TourLog>();

    public TourLogMock()
    {
        TourLog TourLog1 = new TourLog
        {
            Id = 1,
            DateTime = DateTime.Now,
            Comment = "Comment1",
            Difficulty = Difficulty.Easy,
            TotalDistance = 100,
            TotalTime = 120,
            Rating = 5,
            UserId = 1,
            TourId = 1
        };

        TourLog TourLog2 = new TourLog
        {
            Id = 2,
            DateTime = DateTime.Now,
            Comment = "Comment2",
            Difficulty = Difficulty.Medium,
            TotalDistance = 50,
            TotalTime = 60,
            Rating = 4,
            UserId = 1,
            TourId = 1
        };

        TourLog TourLog3 = new TourLog
        {
            Id = 3,
            DateTime = DateTime.Now,
            Comment = "Comment3",
            Difficulty = Difficulty.Hard,
            TotalDistance = 10,
            TotalTime = 30,
            Rating = 3,
            UserId = 1,
            TourId = 1
        };

        if (!TourLogs.Any())
        {
            TourLogs.Add(TourLog1);
            TourLogs.Add(TourLog2);
            TourLogs.Add(TourLog3);
        }
    }

    public void AddTourLog(TourLog tourLog)
    {
        TourLogs.Add(tourLog);
    }

    public void DeleteTourLog(int id)
    {
        var tourLogToDelete = TourLogs.FirstOrDefault(tl => tl.Id == id);
        if (tourLogToDelete != null)
        {
            TourLogs.Remove(tourLogToDelete);
        }
    }

    public List<TourLog> GetAllTourLogs()
    {
        return TourLogs;
    }

    public List<TourLog> GetTourLogsByTourId(int tourId)
    {
        return TourLogs.Where(tl => tl.TourId == tourId).ToList();
    }

    public void UpdateTourLog(TourLog tourLog)
    {
        var tourLogToUpdate = TourLogs.FirstOrDefault(tl => tl.Id == tourLog.Id);
        if (tourLogToUpdate != null)
        {
            tourLogToUpdate.DateTime = tourLog.DateTime;
            tourLogToUpdate.Comment = tourLog.Comment;
            tourLogToUpdate.Difficulty = tourLog.Difficulty;
            tourLogToUpdate.TotalDistance = tourLog.TotalDistance;
            tourLogToUpdate.TotalTime = tourLog.TotalTime;
            tourLogToUpdate.Rating = tourLog.Rating;
            tourLogToUpdate.UserId = tourLog.UserId;
            tourLogToUpdate.TourId = tourLog.TourId;
        }
    }
}
