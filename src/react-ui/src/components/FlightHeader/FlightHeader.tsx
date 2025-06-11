import { Search } from '../Search/Search'
import './FlightHeader.css'

interface FlightHeaderProps {
  setTrackedFlight: (flight: string) => void 
}

export const FlightHeader = ({ setTrackedFlight }: FlightHeaderProps) => {
  return (
    <div className="header">
      <h1 className="title">Flight Details</h1>
      <p className="subtitle">
        Track your flights in real-time with our modern dashboard
      </p>

      <Search onSearch={(query) => setTrackedFlight(query)} />

    </div>
  )
}
