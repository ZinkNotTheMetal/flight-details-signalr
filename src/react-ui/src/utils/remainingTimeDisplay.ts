import dayjs from "./dayjs"

export const remainingTimeDisplay = (start: string, ending: string): string => {
  const difference = dayjs(ending).diff(start)
  const prettyDuration = dayjs.duration(difference)
  const hoursRemaining = prettyDuration.hours()
  const minutesRemaining = prettyDuration.minutes()

  if (hoursRemaining > 0) {
    return `${hoursRemaining} h ${minutesRemaining} min`
  }
  else if (minutesRemaining > 0) {
    return `${minutesRemaining} min`
  }
  else {
    return ''
  }
}