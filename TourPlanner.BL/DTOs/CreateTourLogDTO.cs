using System;

namespace TourPlanner.BL.DTOs;

public class CreateTourLogDTO
{
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string Comment { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public double TotalDistance { get; set; }
    public double TotalTime { get; set; }
    public int Rating { get; set; }
    public int TourId { get; set; } // Die ID der zugehörigen Tour
}