using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL;

public interface IRouteService
{
    Task<RouteInfo> GetRouteAsync(string from, string to, string transportType);
    Task<(double lat, double lon)> GeocodeAsync(string address);
}
