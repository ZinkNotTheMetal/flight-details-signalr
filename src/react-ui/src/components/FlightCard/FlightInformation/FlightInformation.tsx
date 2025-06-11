import type { AircraftDetails, FlightDistance } from "../../../types/flight"

interface FlightInformationProps {
  distance: FlightDistance
  aircraft: AircraftDetails
  airlineCode: string
}


export const FlightInformation = ({ distance, aircraft, airlineCode} :FlightInformationProps) => {
  return (
    <div className="detailSection">
      <div className="sectionTitle">
        Flight Information
      </div>

      <div className="detailGrid">
        <div className="detailItem">
          <div className="detailLabel">Distance</div>
          <div className="detailValue">{distance.miles.toFixed(0) || 'N/A'} miles</div>
        </div>
        <div className="detailItem">
          <div className="detailLabel">Airline Code</div>
          <div className="detailValue">{airlineCode || 'N/A'}</div>
        </div>
        <div className="detailItem">
          <div className="detailLabel">Aircraft Registration</div>
          <div className="detailValue">{aircraft.registrationNumber || 'N/A'}</div>
        </div>
        <div className="detailItem">
          <div className="detailLabel">Aircraft</div>
          <div className="detailValue">{aircraft.model || 'N/A'}</div>
        </div>
      </div>

    </div>
  )
}
