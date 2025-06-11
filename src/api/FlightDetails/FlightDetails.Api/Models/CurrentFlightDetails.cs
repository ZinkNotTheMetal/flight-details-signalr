namespace FlightDetails.Api.Models;

public class CurrentFlightDetails
{
    public string Status { get; set; } = string.Empty;
    
    public CurrentFlightLocation Location { get; set; } = new();

    public Times Departure { get; set; } = new();
    
    public Times Arrival { get; set; } = new();
}

public class Times
{
    public string? ScheduledUtc { get; set; }
    
    public string? RevisedUtc { get; set; }

    public string? RunwayUtc { get; set; }

    public string? PredictedUtc { get; set; } = null!;
}

public class CurrentFlightLocation
{
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public Track Track { get; set; } = new();
    
    public CurrentFlightSpeed Speed { get; set; } = new();
    
    public string UpdatedAtUtc { get; set; } = string.Empty;
}

public class CurrentFlightSpeed
{
    public double Knots { get; set; }
    public double KilometersPerHour { get; set; }
    public double MilesPerHour { get; set; }
    public double MetersPerSecond { get; set; }
}

public class Track
{
    public int Degrees { get; set; }
    public double Radians { get; set; }
}