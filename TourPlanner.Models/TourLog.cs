using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models;

public class TourLog
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Comment { get; set; }
    public int Difficulty { get; set; }
    public double TotalDistance { get; set; }
    public double TotalTime { get; set; }
    public int Rating { get; set; }
    public int UserId { get; set; }
    public int TourId { get; set; }
}
