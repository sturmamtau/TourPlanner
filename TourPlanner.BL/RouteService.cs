using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace TourPlanner.BL;


public class RouteInfo
{
    public double Distance { get; set; }  // in Metern
    public double Duration { get; set; }  // in Sekunden
    public string GeoJson { get; set; } = "";
}


public class RouteService : IRouteService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RouteService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;

        var apiKey = config["OpenRouteService:ApiKey"];

        // early warning if api key missing
        if (apiKey == null)
        {
            throw new ArgumentNullException($"could not find API Key");
        }
        _apiKey = apiKey;
    }

    public async Task<(double lat, double lon)> GeocodeAsync(string address)
    {
        var url = $"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={Uri.EscapeDataString(address)}&size=1";
        var response = await _httpClient.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        var coords = json.RootElement
            .GetProperty("features")[0]
            .GetProperty("geometry")
            .GetProperty("coordinates");
        return (coords[1].GetDouble(), coords[0].GetDouble()); // lat, lon
    }

    public async Task<RouteInfo> GetRouteAsync(string from, string to, string transportType)
    {
        var (fromLat, fromLon) = await GeocodeAsync(from);
        var (toLat, toLon) = await GeocodeAsync(to);

        var profile = transportType switch
        {
            "Car" => "driving-car",
            "Bike" => "cycling-regular",
            "Run" => "foot-hiking",
            "Walk" => "foot-walking",
            _ => "driving-car"
        };

        var url = $"https://api.openrouteservice.org/v2/directions/{profile}?api_key={_apiKey}" +
        $"&start={fromLon.ToString(CultureInfo.InvariantCulture)},{fromLat.ToString(CultureInfo.InvariantCulture)}" +
        $"&end={toLon.ToString(CultureInfo.InvariantCulture)},{toLat.ToString(CultureInfo.InvariantCulture)}";

        var httpResponse = await _httpClient.GetAsync(url);
        var responseBody = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"ORS Directions error ({httpResponse.StatusCode}): {responseBody}");
        }

        var json = JsonDocument.Parse(responseBody);
        var summary = json.RootElement
            .GetProperty("features")[0]
            .GetProperty("properties")
            .GetProperty("summary");

        return new RouteInfo
        {
            Distance = summary.GetProperty("distance").GetDouble(),
            Duration = summary.GetProperty("duration").GetDouble(),
            GeoJson = responseBody
        };
    }

    public async Task<byte[]> DownloadStaticMapAsync(double fromLat, double fromLon, double toLat, double toLon)
    {
        // 
        string url = $"https://maps.geoapify.com/v1/staticmap?style=osm-carto&width=600&height=400&center=lonlat:{(fromLon + toLon) / 2},{(fromLat + toLat) / 2}&zoom=12&marker=lonlat:{fromLon},{fromLat};color:%23ff0000;size:medium|lonlat:{toLon},{toLat};color:%2300ff00;size:medium&apiKey=DEIN_GEOAPIFY_API_KEY";

        // Alternativ kannst du auch das GeoJSON an einen kompatiblen Dienst senden.

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Fehler beim Laden des statischen Kartenbildes.");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }

}
