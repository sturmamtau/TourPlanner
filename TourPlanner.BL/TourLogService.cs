using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.Models;
using TourPlanner.DAL.Repositories;
using TourPlanner.BL.DTOs;

namespace TourPlanner.BL;

public class TourLogService : ITourLogService
{
    private readonly ITourLogRepository _logRepo;

    public TourLogService(ITourLogRepository logRepo)
    {
        _logRepo = logRepo;
    }

    public List<TourLogDTO> GetLogsForTour(int tourId)
    {
        var logs = _logRepo.GetLogsByTourId(tourId);
        return logs.Select(log => MapToDTO(log)).ToList();
    }

    public TourLogDTO AddTourLog(CreateTourLogDTO dto)
    {
        var log = new TourLog
        {
            DateTime = dto.DateTime.ToUniversalTime(),
            Comment = dto.Comment,
            Difficulty = (Difficulty)dto.Difficulty,
            TotalDistance = dto.TotalDistance,
            TotalTime = dto.TotalTime,
            Rating = dto.Rating,
            TourId = dto.TourId
        };

        var addedLog = _logRepo.Add(log);
        return MapToDTO(addedLog);
    }

    public void UpdateTourLog(int id, CreateTourLogDTO dto)
    {
        var logToUpdate = _logRepo.GetById(id);
        if (logToUpdate == null) throw new KeyNotFoundException($"Log mit ID {id} existiert nicht.");

        logToUpdate.DateTime = dto.DateTime.ToUniversalTime();
        logToUpdate.Comment = dto.Comment;
        logToUpdate.Difficulty = (Difficulty)dto.Difficulty;
        logToUpdate.TotalDistance = dto.TotalDistance;
        logToUpdate.TotalTime = dto.TotalTime;
        logToUpdate.Rating = dto.Rating;
        logToUpdate.TourId = dto.TourId;

        _logRepo.Update(logToUpdate);
    }

    public void DeleteTourLog(int id)
    {
        var log = _logRepo.GetById(id);
        if (log == null) throw new KeyNotFoundException($"Log mit ID {id} existiert nicht.");
        _logRepo.Delete(log);
    }

    // Hilfsmethode fürs Mapping
    private TourLogDTO MapToDTO(TourLog log)
    {
        return new TourLogDTO
        {
            Id = log.Id,
            DateTime = log.DateTime,
            Comment = log.Comment ?? "",
            Difficulty = (int)log.Difficulty,
            TotalDistance = log.TotalDistance,
            TotalTime = log.TotalTime,
            Rating = log.Rating,
            TourId = log.TourId
        };
    }
}