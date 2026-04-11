using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models;

public class TourLog
{
    public int Id { get; set; }
    [Required]
    public DateTime DateTime { get; set; }
    [StringLength(500)]
    public string? Comment { get; set; }
    [Required]
    public Difficulty Difficulty { get; set; }
    [Range(0, 1000000)]
    public double TotalDistance { get; set; }
    [Range(0, 20160)]
    public double TotalTime { get; set; }
    [Range(1,5)]
    public int Rating { get; set; }
    [Required]
    public int TourId { get; set; }
}
