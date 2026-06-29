using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.DTOs;

public class GetTourDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string TransportType { get; set; } = string.Empty;
    public double TourDistance { get; set; }
    public int EstimatedTime { get; set; }

    public int Popularity { get; set; }
    public int ChildFriendliness { get; set; }
    // url for tourmap, ready for frontend
    public string? ImageUrl { get; set; }
}
