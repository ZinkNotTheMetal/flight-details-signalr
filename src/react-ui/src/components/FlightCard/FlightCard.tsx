import { CardHeader } from "./CardHeader/CardHeader"
import { FlightRoute } from "./FlightRoute/FlightRoute"
import './FlightCard.css'
import { useEffect, useState } from "react"
import type { Flight } from "../../types/flight"
import type { LiveDetails } from "../../types/live-details"
import dayjs from "../../utils/dayjs"
import { FlightInformation } from "./FlightInformation/FlightInformation"
import { FlightLocation } from "./FlightLocation/FlightLocation"

interface FlightCardProps {
  signalRConnection: signalR.HubConnection | null
  flightNumber: string
}

export const FlightCard = ({ signalRConnection, flightNumber }: FlightCardProps) => {
  const [staticFlightDetails, setStaticFlightDetails] = useState<Flight | null>(null)
  const [liveFlightDetails, setLiveFlightDetails] = useState<LiveDetails | null>(null)

  useEffect(() => {
    if (!signalRConnection) return

    const getLiveDetailsAsync = async() => {
      if (flightNumber) {
        await signalRConnection.invoke("WatchFlight", flightNumber)
      }
    }

    const getFlightDetailsAsync = async () => {
      const staticDetailsResponse = await fetch(`https://localhost:7272/api/flight/${flightNumber}`)
      const json = await staticDetailsResponse.json()

      setStaticFlightDetails(json)
    }

    getFlightDetailsAsync()
    getLiveDetailsAsync()

    signalRConnection.on("LiveFlightDetailsUpdate", (flightNumber: string, details: LiveDetails) => {
      setLiveFlightDetails(details)
    })
  }, [flightNumber])

  return (
    <div className="flightCard">
      {staticFlightDetails && (
        <>
          <CardHeader 
            status={liveFlightDetails?.status || 'N/A'} 
            lastUpdated={dayjs(liveFlightDetails?.location.updatedAtUtc).utc().format('LLL') || dayjs().format('LLL')} 
            airline={staticFlightDetails.airlineDetails.name} 
            flightNumber={flightNumber} />

          <FlightRoute 
            departure={staticFlightDetails.departure} 
            arrival={staticFlightDetails.arrival}
            departureTimeUtc={liveFlightDetails?.departure.runwayUtc || liveFlightDetails?.departure.revisedUtc || liveFlightDetails?.departure.predictedUtc || 'N/A'}
            arrivalTimeUtc={liveFlightDetails?.arrival.runwayUtc || liveFlightDetails?.arrival.revisedUtc || liveFlightDetails?.arrival.predictedUtc || liveFlightDetails?.arrival.scheduledUtc || 'N/A'}
          />

          <FlightInformation
            distance={staticFlightDetails.flightDistance}
            aircraft={staticFlightDetails.aircraftDetails}
            airlineCode={staticFlightDetails.airlineDetails.icaoCode}
          />

          <FlightLocation
            latitude={liveFlightDetails?.location.latitude || 0}
            longitude={liveFlightDetails?.location.longitude || 0}
            trackDegrees={liveFlightDetails?.location.track.degrees || 0}
            speed={liveFlightDetails?.location.speed}
          />
        </>
      )}

    </div>
  )
}
