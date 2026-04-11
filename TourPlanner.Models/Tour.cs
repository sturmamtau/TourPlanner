using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models;

public class Tour
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    [Required]
    public string From { get; set; }
    [Required]
    public string To { get; set; }
    [Required]
    public TransportType TransportType { get; set; }
    [Range(0, 1000000)]
    public int TourDistance { get; set; }
    [Range(0, 20160)]
    public int EstimatedTime { get; set; }
    public string? ImagePath { get; set; }
    [Required]
    public int UserId { get; set; }
    public ICollection<TourLog>? TourLogs { get; set; }
}
