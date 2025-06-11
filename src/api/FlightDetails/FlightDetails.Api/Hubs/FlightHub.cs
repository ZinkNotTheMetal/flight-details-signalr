using FlightDetails.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace FlightDetails.Api.Hubs;

public class FlightHub(IFlightConnectionTracker tracker) : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Successfully Connected: {Context.ConnectionId}");
        await Clients.All.SendAsync("Receive Message", $"{Context.ConnectionId} has successfully connected.");
        
        await base.OnConnectedAsync();
    }

    public async Task WatchFlight(string flightNumber)
    {
        tracker.TrackFlight(flightNumber, Context.ConnectionId);
        // Check status (only add to group if not arrived
        await Groups.AddToGroupAsync(Context.ConnectionId, flightNumber);
    }

    public async Task UnwatchFlight(string flightNumber)
    {
        tracker.RemoveFlightTracking(flightNumber, Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, flightNumber);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        tracker.RemoveConnection(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}