using FlightDetails.Api.Models;

namespace FlightDetails.Api.Interfaces;

public interface IFlightUpdateService
{
    Task<Flight?> GetFlightDetails(string flightNumber);

    Task<CurrentFlightDetails?> GetLiveFlightDetails(string flightNumber);
}