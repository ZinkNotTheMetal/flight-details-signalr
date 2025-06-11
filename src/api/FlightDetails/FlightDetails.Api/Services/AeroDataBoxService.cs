using System.Text.Json;
using FlightDetails.Api.Interfaces;
using FlightDetails.Api.Models;
using FlightDetails.Api.Models.AeroDataBox;
using Airline = FlightDetails.Api.Models.Airline;

namespace FlightDetails.Api.Services;

public class AeroDataBoxService(IHttpClientFactory httpClientFactory, string apiKey) : IFlightUpdateService
{
    private readonly string _aeroDataBoxBaseUrl = $"https://aerodatabox.p.rapidapi.com/";

    public async Task<Flight?> GetFlightDetails(string flightNumber)
    {
        var httpClient = httpClientFactory.CreateClient("AeroDataBox");

        httpClient.BaseAddress = new Uri(_aeroDataBoxBaseUrl);
        httpClient.DefaultRequestHeaders.Add("x-rapid-host", "aerodatabox.p.rapidapi.com");
        httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);

        try
        {
            // Documentation:
            // https://rapidapi.com/aedbx-aedbx/api/aerodatabox/playground/apiendpoint_37f1f719-ef9e-4596-abf6-b3f882435e4e
            // Do not get live details here
            var response = await httpClient.GetAsync($"flights/number/{flightNumber}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                AeroDataBoxFlightResponse[]? aeroDataBoxFlightResponse = null;

                try
                {
                    aeroDataBoxFlightResponse = JsonSerializer.Deserialize<AeroDataBoxFlightResponse[]>(
                        json.Replace("\r", "").Replace("\n", ""), 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (aeroDataBoxFlightResponse == null) return null;
                    var relevantFlight = aeroDataBoxFlightResponse.First();
                    
                    return new Flight 
                    {
                        AirlineDetails = new Airline
                        {
                            IataCode = relevantFlight.Airline.Iata,
                            IcaoCode = relevantFlight.Airline.Icao,
                            Name = relevantFlight.Airline.Name,
                        },
                        Arrival = new Airport
                        {
                            Iata = relevantFlight.Arrival.Airport.Iata,
                            Icao = relevantFlight.Arrival.Airport.Icao,
                            Name = relevantFlight.Arrival.Airport.Name,
                            ShortName = relevantFlight.Arrival.Airport.ShortName,
                            TimeZone = relevantFlight.Arrival.Airport.TimeZone,
                        },
                        Departure = new Airport
                        {
                            Iata = relevantFlight.Departure.Airport.Iata,
                            Icao = relevantFlight.Departure.Airport.Icao,
                            Name = relevantFlight.Departure.Airport.Name,
                            ShortName = relevantFlight.Departure.Airport.ShortName,
                            TimeZone = relevantFlight.Departure.Airport.TimeZone,
                        },
                        AircraftDetails = new Aircraft
                        {
                            Model = relevantFlight.Aircraft.Model,
                            RegistrationNumber = relevantFlight.Aircraft.Reg,
                        },
                        FlightDistance = new Distance
                        {
                            Kilometers = relevantFlight.GreatCircleDistance.Km,
                            Miles = relevantFlight.GreatCircleDistance.Mile,
                            NauticalMiles = relevantFlight.GreatCircleDistance.Nm
                        },
                        FlightNumber = relevantFlight.Number
                    };
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deserialization failed: " + ex.Message);
                    return null;
                }

                return new Flight();
                
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<CurrentFlightDetails?> GetLiveFlightDetails(string flightNumber)
    {
        var httpClient = httpClientFactory.CreateClient("AeroDataBox");

        httpClient.BaseAddress = new Uri(_aeroDataBoxBaseUrl);
        httpClient.DefaultRequestHeaders.Add("x-rapid-host", "aerodatabox.p.rapidapi.com");
        httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);

        try
        {
            // Documentation:
            // https://rapidapi.com/aedbx-aedbx/api/aerodatabox/playground/apiendpoint_37f1f719-ef9e-4596-abf6-b3f882435e4e
            // Get Live Details Here 
            var response = await httpClient.GetAsync($"flights/number/{flightNumber}/{DateTime.Now:yyyy-MM-dd}?withLocation=true");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                AeroDataBoxFlightResponse[]? aeroDataBoxLocationResponse = null;

                try
                {
                    Console.WriteLine(json);
                    aeroDataBoxLocationResponse = JsonSerializer.Deserialize<AeroDataBoxFlightResponse[]>(
                        json?.Replace("\r", "")?.Replace("\n", "") ?? string.Empty,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (aeroDataBoxLocationResponse == null) return null;
                    
                    var relevantFlight = aeroDataBoxLocationResponse.First(w => w.Status == "EnRoute");
                    Console.WriteLine(relevantFlight.Status);
                    
                    return new CurrentFlightDetails
                    {
                        Arrival = new Times
                        {
                            PredictedUtc = relevantFlight.Arrival.PredictedTime.Utc,
                            RevisedUtc = relevantFlight.Arrival.RevisedTime.Utc,
                            RunwayUtc = relevantFlight.Arrival.RunwayTime.Utc,
                            ScheduledUtc = relevantFlight.Arrival.ScheduledTime.Utc,
                        },
                        Departure = new Times
                        {
                            PredictedUtc = relevantFlight.Departure.RevisedTime.Utc,
                            RevisedUtc = relevantFlight.Departure.RevisedTime.Utc,
                            RunwayUtc = relevantFlight.Departure.RunwayTime.Utc,
                            ScheduledUtc = relevantFlight.Departure.ScheduledTime.Utc,
                        },
                        Location = new CurrentFlightLocation
                        {
                            Latitude = relevantFlight.Location.Lat,
                            Longitude = relevantFlight.Location.Lon,
                            Speed = new CurrentFlightSpeed
                            {
                                Knots = relevantFlight.Location.GroundSpeed.Kt,
                                KilometersPerHour = relevantFlight.Location.GroundSpeed.KmPerHour,
                                MetersPerSecond = relevantFlight.Location.GroundSpeed.MeterPerSecond,
                                MilesPerHour = relevantFlight.Location.GroundSpeed.MiPerHour,
                            },
                            UpdatedAtUtc = relevantFlight.Location.ReportedAtUtc,
                        },
                        Status = relevantFlight.Status
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deserialization failed: " + ex.Message);
                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}