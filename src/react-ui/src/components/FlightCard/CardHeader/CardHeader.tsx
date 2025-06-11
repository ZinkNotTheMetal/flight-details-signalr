import dayjs from '../../../utils/dayjs'
import './CardHeader.css'

const getStatusClass = (status: string) => {
  switch (status?.toLowerCase()) {
    case 'on time':
    case 'scheduled':
      return 'onTime';
    case 'delayed':
      return 'delayed';
    case 'boarding':
      return 'boarding';
    case 'en route':
    case 'airborne':
      return 'enRoute';
    default:
      return 'onTime';
  }
};

interface CardHeaderProps {
  status: string
  lastUpdated: string
  airline: string
  flightNumber: string
}

export const CardHeader = ({ status, lastUpdated, airline, flightNumber }: CardHeaderProps) => {
  return (
    <div className="flightHeader">
      <div className="headerLeft">
        <div className="airline">{airline}</div>
        <div className="flightNumber">{flightNumber}</div>
      </div>
      <div className="statusContainer">
        <div className={`statusBadge ${getStatusClass(status)}`}>
          {status}
        </div>
        <div className="lastUpdate">
          {dayjs(lastUpdated).format('')}
        </div>
      </div>
    </div>
  )
}
