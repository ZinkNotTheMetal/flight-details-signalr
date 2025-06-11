using Newtonsoft.Json;

namespace FlightDetails.Api.Models.AeroDataBox;

public class AeroDataBoxFlightResponse
{
    [JsonProperty("greatCircleDistance")]
    public GreatCircleDistance GreatCircleDistance { get; set; } = new();
    
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
    
    [JsonProperty("airline")]
    public Airline Airline { get; set; } = new();
    
    [JsonProperty("location")]
    public CurrentAircraftLocation Location { get; set; } = new();
    
    [JsonProperty("departure")]
    public Departure Departure { get; set; } = new();

    [JsonProperty("arrival")]
    public Arrival Arrival { get; set; } = new();
    
    [JsonProperty("aircraft")]
    public AeroDataBoxAircraft Aircraft { get; set; } = new();
    
    [JsonProperty("number")]
    public string Number { get; set; } = string.Empty;

    [JsonProperty("callSign")]
    public string CallSign { get; set; } = string.Empty;
}

public class Airline
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("iata")]
    public string Iata { get; set; } = string.Empty;

    [JsonProperty("icao")]
    public string Icao { get; set; } = string.Empty;
}

public class CurrentAircraftAltitude
{
    [JsonProperty("meter")]
    public double Meter { get; set; }

    [JsonProperty("km")]
    public double Km { get; set; }

    [JsonProperty("mile")]
    public double Mile { get; set; }

    [JsonProperty("nm")]
    public double Nm { get; set; }

    [JsonProperty("feet")]
    public double Feet { get; set; }
}

public class CurrentAircraftLocation
{
    [JsonProperty("pressureAltitude")]
    public CurrentAircraftAltitude Altitude { get; set; } = new();
    
    [JsonProperty("groundSpeed")]
    public CurrentAircraftSpeed GroundSpeed { get; set; } = new();
    
    [JsonProperty("trueTrack")]
    public CurrentAircraftTrack TrueTrack { get; set; } = new();
    
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lon")]
    public double Lon { get; set; }
    
    [JsonProperty("reportedAtUtc")]
    public string ReportedAtUtc { get; set; } = string.Empty;
}

public class CurrentAircraftTrack
{
    [JsonProperty("deg")]
    public double Deg { get; set; }

    [JsonProperty("rad")]
    public double Rad { get; set; }
}

public class CurrentAircraftSpeed
{
    [JsonProperty("kt")]
    public double Kt { get; set; }

    [JsonProperty("kmPerHour")]
    public double KmPerHour { get; set; }

    [JsonProperty("miPerHour")]
    public double MiPerHour { get; set; }

    [JsonProperty("meterPerSecond")]
    public double MeterPerSecond { get; set; }
}

public class Arrival
{
    [JsonProperty("airport")]
    public AeroDataBoxAirport Airport { get; set; } = new();
    
    [JsonProperty("scheduledTime")]
    public Time ScheduledTime { get; set; } = new();

    [JsonProperty("revisedTime")]
    public Time RevisedTime { get; set; } = new();

    [JsonProperty("predictedTime")]
    public Time PredictedTime { get; set; } = new();
    
    [JsonProperty("runwayTime")]
    public Time RunwayTime { get; set; } = new();
}

public class AeroDataBoxAirport
{
    [JsonProperty("icao")]
    public string Icao { get; set; } = string.Empty;

    [JsonProperty("iata")]
    public string Iata { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("shortName")]
    public string ShortName { get; set; } = string.Empty;

    [JsonProperty("municipalityName")]
    public string MunicipalityName { get; set; } = string.Empty;

    [JsonProperty("countryCode")]
    public string CountryCode { get; set; } = string.Empty;

    [JsonProperty("timeZone")]
    public string TimeZone { get; set; } = string.Empty;
}

public class Departure
{
    [JsonProperty("airport")]
    public AeroDataBoxAirport Airport { get; set; } = new();
    
    [JsonProperty("scheduledTime")]
    public Time ScheduledTime { get; set; } = new();

    [JsonProperty("revisedTime")]
    public Time RevisedTime { get; set; } = new();

    [JsonProperty("runwayTime")]
    public Time RunwayTime { get; set; } = new();
}

public class AeroDataBoxAircraft
{
    [JsonProperty("reg")]
    public string Reg { get; set; } = string.Empty;

    [JsonProperty("modeS")]
    public string ModeS { get; set; } = string.Empty;

    [JsonProperty("model")]
    public string Model { get; set; } = string.Empty;
}

public class Time
{
    [JsonProperty("utc")]
    public string Utc { get; set; } = string.Empty;

    [JsonProperty("local")]
    public string Local { get; set; } = string.Empty;
}

public class GreatCircleDistance
{
    [JsonProperty("meter")]
    public double Meter { get; set; }

    [JsonProperty("km")]
    public double Km { get; set; }

    [JsonProperty("mile")]
    public double Mile { get; set; }

    [JsonProperty("nm")]
    public double Nm { get; set; }

    [JsonProperty("feet")]
    public double Feet { get; set; }
}