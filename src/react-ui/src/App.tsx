import { useEffect, useState } from "react"
import { FlightHeader } from "./components/FlightHeader/FlightHeader"
import * as signalR from '@microsoft/signalr'
import './App.css'
import { FlightCard } from "./components/FlightCard/FlightCard"

function App() {
  const [trackedFlight, setTrackedFlight] = useState<string | null>(null)
  const [signalRConnection, setSignalRConnection] = useState<signalR.HubConnection | null>(null)
  const [isConnected, setIsConnected] = useState<boolean>(false)

  useEffect(() => {
    const signalRConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7272/flight-hub")
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build()

    signalRConnection.onreconnecting(() => {
      console.log("SignalR reconnecting...")
      setIsConnected(false)
    })

    signalRConnection.onclose(() => {
      console.log("SignalR connection closed")
      setIsConnected(false)
    })

    const startConnection = async () => {
      try {
        await signalRConnection.start()
        console.log("SignalR Connected")
        setSignalRConnection(signalRConnection)
        setIsConnected(true)
      } catch (err) {
        console.error("SignalR Connection Error: ", err)
        setTimeout(startConnection, 5000)
      }
    }

    startConnection()

    return () => {
      if (signalRConnection) {
        if (trackedFlight) {
          signalRConnection.invoke("UnwatchFlight", trackedFlight)
            .catch(err => console.error("Error un-watching flight on cleanup:", err))
        }
        signalRConnection.stop()
          .then(() => console.log("SignalR connection stopped"))
          .catch(err => console.error("Error stopping SignalR connection:", err))
      }
    }
  }, [])

  return (
    <div className="container background">
      <FlightHeader setTrackedFlight={(flight) => setTrackedFlight(flight)} />

      {trackedFlight && (
        <FlightCard signalRConnection={signalRConnection} flightNumber={trackedFlight} />
      )}

      <span>{trackedFlight}</span>

      <span>SignalR Connection: {isConnected.toString()}</span>
      
    </div>
  );
}

export default App;
