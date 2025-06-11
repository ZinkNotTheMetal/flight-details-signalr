export interface LiveDetails {
  status:    string;
  location:  Location;
  departure: Arrival;
  arrival:   Arrival;
}

export interface Arrival {
  scheduledUtc: string;
  revisedUtc:   string;
  runwayUtc:    string;
  predictedUtc: string;
}

export interface Location {
  latitude:     number;
  longitude:    number;
  track:        Track;
  speed:        Speed;
  updatedAtUtc: string;
}

export interface Speed {
  knots:             number;
  kilometersPerHour: number;
  milesPerHour:      number;
  metersPerSecond:   number;
}

export interface Track {
  degrees: number;
  radians: number;
}