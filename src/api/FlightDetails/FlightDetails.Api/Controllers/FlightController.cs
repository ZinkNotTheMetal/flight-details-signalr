using FlightDetails.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightDetails.Api.Controllers;

[Route("api/[controller]")]
public class FlightController(IFlightUpdateService flightService) : ControllerBase
{
    [HttpGet("{flightNumber}")]
    public async Task<IActionResult> GetFlightDetails(string flightNumber)
    {
        var response = await flightService.GetFlightDetails(flightNumber);
        return Ok(response);
    }

    [HttpGet("live/{flightNumber}")]
    public async Task<IActionResult> GetLiveFlightDetails(string flightNumber)
    {
        var response = await flightService.GetLiveFlightDetails(flightNumber);
        return Ok(response);
    }
}