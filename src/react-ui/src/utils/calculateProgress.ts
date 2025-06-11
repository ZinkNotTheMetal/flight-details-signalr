import dayjs from "./dayjs"

export const calculateProgressBar = (takeoffTimeUtc: string, arrivalTimeUtc: string): number => {
  const totalDuration = dayjs(arrivalTimeUtc).diff(dayjs(takeoffTimeUtc))
  const elapsedDuration = dayjs().diff(dayjs(takeoffTimeUtc))

  const progressPercentage = (elapsedDuration / totalDuration) * 100
  return Math.min(Math.max(progressPercentage, 0), 100)
}