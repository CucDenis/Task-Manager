
namespace TaskManager.Domain.Models;

public class Location
{
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
