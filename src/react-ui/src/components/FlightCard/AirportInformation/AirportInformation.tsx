import dayjs from "../../../utils/dayjs"
import './AirportInformation.css'

interface AirportInfoProps {
  icaoCode: string
  name: string
  revisedTime: string
  type: 'Departure' | 'Arrival'
}

export const AirportInfo = ({ icaoCode, name, revisedTime, type }: AirportInfoProps) => (
  <div className="airport">
    <div className="airportCode">{icaoCode}</div>
    <div className="airportName">{name}</div>
    <div className="timeContainer">
      <div className="timeLabel">{type}</div>
      <div className="timeValue">
        {dayjs(revisedTime).format('LLL')}
      </div>
    </div>
  </div>
);