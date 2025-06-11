export interface Flight {
  flightNumber:    string;
  flightDistance:  FlightDistance;
  airlineDetails:  AirlineDetails;
  aircraftDetails: AircraftDetails;
  departure:       Airport;
  arrival:         Airport;
}

export interface AircraftDetails {
  registrationNumber: string;
  model:              string;
}

export interface AirlineDetails {
  name:     string;
  iataCode: string;
  icaoCode: string;
}

export interface Airport {
  icao:      string;
  iata:      string;
  name:      string;
  shortName: string;
  timeZone:  string;
}

export interface FlightDistance {
  kilometers:    number;
  miles:         number;
  nauticalMiles: number;
}
