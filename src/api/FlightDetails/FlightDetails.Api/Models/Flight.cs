namespace FlightDetails.Api.Models;

public class Flight
{
    public string FlightNumber { get; set; } = string.Empty;

    public Distance FlightDistance { get; set; } = new();

    public Airline AirlineDetails { get; set; } = new();
    
    public Aircraft AircraftDetails { get; set; } = new();

    public Airport Departure { get; set; } = new();

    public Airport Arrival { get; set; } = new();
}

public class Airline
{
    public string Name { get; set; } = string.Empty;
    public string IataCode { get; set; } = string.Empty;
    public string IcaoCode { get; set; } = string.Empty;
}

public class Distance
{
    public double Kilometers { get; set; }
    public double Miles { get; set; }
    public double NauticalMiles { get; set; }
}

public class Airport
{
    public string Icao { get; set; } = string.Empty;
    public string Iata { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string TimeZone { get; set; } = string.Empty;
}

public class Aircraft
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
}