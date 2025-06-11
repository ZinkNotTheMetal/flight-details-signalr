interface ProgressProps {
  percentage: number
}

export const Progress = ({ percentage }: ProgressProps) => {
  return (
    <div className="planeIconContainer">
      <div className="planeIcon">
        {/* <Plane size={28} color="white" /> */}
      </div>
      <div className="flightProgress">
        <div 
          className="progressBar" 
          style={{ width: `${percentage}%` }}
        ></div>
      </div>
    </div>
  )
}
