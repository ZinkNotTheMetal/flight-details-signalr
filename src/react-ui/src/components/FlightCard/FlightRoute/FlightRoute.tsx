import type { Airport } from "../../../types/flight";
import { AirportInfo } from "../AirportInformation/AirportInformation";
import { Progress } from "../Progress/Progress";

interface FlightRouteProps {
  departure: Airport
  arrival: Airport
  departureTimeUtc: string
  arrivalTimeUtc: string
}

export const FlightRoute = ({ departure, arrival, departureTimeUtc, arrivalTimeUtc }: FlightRouteProps) => (
  <div className="routeContainer">
    <AirportInfo 
      icaoCode={departure.icao}
      name={departure.name}
      revisedTime={departureTimeUtc}
      type="Departure"
    />

    <Progress percentage={50} />
    
    <AirportInfo
      icaoCode={arrival.icao}
      name={arrival.name}
      revisedTime={arrivalTimeUtc}
      type="Arrival"
    />
  </div>
);