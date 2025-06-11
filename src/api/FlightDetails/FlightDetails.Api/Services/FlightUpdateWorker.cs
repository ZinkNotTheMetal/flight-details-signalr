using FlightDetails.Api.Hubs;
using FlightDetails.Api.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FlightDetails.Api.Services;

public class FlightUpdateWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFlightConnectionTracker _tracker;
    private readonly ILogger<FlightUpdateWorker> _logger;

    public FlightUpdateWorker(
        IServiceProvider serviceProvider, 
        IFlightConnectionTracker tracker,
        ILogger<FlightUpdateWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _tracker = tracker;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FlightUpdateWorker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Create a new service scope for each iteration
                using var scope = _serviceProvider.CreateScope();
                var flightService = scope.ServiceProvider.GetRequiredService<IFlightUpdateService>();
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<FlightHub>>();

                var watchedFlights = _tracker.GetWatchedFlights().ToList();
                _logger.LogDebug("Checking {FlightCount} watched flights", watchedFlights.Count);

                foreach (var flightNumber in watchedFlights.Where(flightNumber => _tracker.FlightHasWatchers(flightNumber)))
                {
                    try
                    {
                        var liveFlightDetails = await flightService.GetLiveFlightDetails(flightNumber);
                        if (liveFlightDetails == null) continue;
                        await hubContext.Clients.Group(flightNumber)
                            .SendAsync("LiveFlightDetailsUpdate", flightNumber, liveFlightDetails, stoppingToken);
                                
                        _logger.LogDebug("Sent update for flight {FlightNumber}", flightNumber);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error updating flight {FlightNumber}", flightNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FlightUpdateWorker iteration");
            }

            await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }

        _logger.LogInformation("FlightUpdateWorker stopped");
    }
}
