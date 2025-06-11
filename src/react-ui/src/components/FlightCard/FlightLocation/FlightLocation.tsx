import type { Speed } from "../../../types/live-details"

interface FlightLocationProps {
  speed: Speed | undefined
  latitude: number
  longitude: number
  trackDegrees: number
}

export const FlightLocation = ({ speed, latitude, longitude, trackDegrees }: FlightLocationProps) => {
  return (
    <div className="detailSection">
      <div className="sectionTitle">
        Current Location
      </div>

      <div className="detailGrid">
        <div className="detailItem">
          <div className="detailLabel">Speed</div>
          <div className="detailValue">{speed?.milesPerHour.toFixed(0) || 'N/A'} mph ({speed?.knots} kt)</div>
        </div>
        <div className="detailItem">
          <div className="detailLabel">Latitude</div>
          <div className="detailValue">{latitude}</div>
        </div>
        <div className="detailItem">
          <div className="detailLabel">Longitude</div>
          <div className="detailValue">{longitude}</div>
        </div>
        <div className="detailItem">
          <div className="detailLabel">Track</div>
          <div className="detailValue">{trackDegrees}</div>
        </div>
      </div>

    </div>
  )
}
