using System.Collections.Concurrent;

namespace FlightDetails.Api.Services;

public interface IFlightConnectionTracker
{
    void TrackFlight(string flightNumber, string connectionId);
    void RemoveFlightTracking(string flightNumber, string connectionId);
    void RemoveConnection(string connectionId);
    bool FlightHasWatchers(string flightNumber);
    IEnumerable<string> GetWatchedFlights();
}

public class FlightConnectionTracker : IFlightConnectionTracker
{
    private readonly ConcurrentDictionary<string, ConcurrentBag<string>> _flightConnections = new();

    public void TrackFlight(string flightNumber, string connectionId)
    {
        _flightConnections.AddOrUpdate(
            flightNumber,
            new ConcurrentBag<string> { connectionId },
            (key, existingBag) =>
            {
                existingBag.Add(connectionId);
                return existingBag;
            });
    }

    public void RemoveFlightTracking(string flightNumber, string connectionId)
    {
        if (_flightConnections.TryGetValue(flightNumber, out var connections))
        {
            var newBag = new ConcurrentBag<string>(connections.Where(id => id != connectionId));

            if (newBag.IsEmpty)
            {
                _flightConnections.TryRemove(flightNumber, out _);
            }
            else
            {
                _flightConnections.TryUpdate(flightNumber, newBag, connections);
            }
        }
    }

    public void RemoveConnection(string connectionId)
    {
        var keysToUpdate = new List<string>();
        foreach (var kvp in _flightConnections)
        {
            if (kvp.Value.Contains(connectionId))
            {
                keysToUpdate.Add(kvp.Key);
            }
        }

        foreach (var flightNumber in keysToUpdate)
        {
            RemoveFlightTracking(flightNumber, connectionId);
        }
    }

    public bool FlightHasWatchers(string flightNumber)
    {
        return _flightConnections.TryGetValue(flightNumber, out var connections) && !connections.IsEmpty;
    }

    public IEnumerable<string> GetWatchedFlights()
    {
        return _flightConnections.Keys.ToList();
    }
}