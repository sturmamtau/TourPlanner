namespace TourPlanner.BL.DTOs;

public class TourLogDTO
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public double TotalDistance { get; set; }
    public double TotalTime { get; set; }
    public int Rating { get; set; }
    public int TourId { get; set; }
}