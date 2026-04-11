using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public TransportType TransportType { get; set; }
    public int TourDistance { get; set; }
    public int EstimatedTime { get; set; }
    public string ImagePath { get; set; }

    public int UserId { get; set; }
    public ICollection<TourLog>? TourLogs { get; set; }
}
