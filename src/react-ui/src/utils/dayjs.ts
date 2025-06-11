import dayjs from "dayjs"
import advancedFormat from "dayjs/plugin/advancedFormat"
import localizedFormat from "dayjs/plugin/localizedFormat"
import isToday from "dayjs/plugin/isToday"
import relativeTime from "dayjs/plugin/relativeTime"
import timezone from "dayjs/plugin/timezone"
import utc from "dayjs/plugin/utc"
import duration from "dayjs/plugin/duration"

// Add additional functionality to day.js
// If we need this in multiple places I suggest we pull
//  it out of this component and put it in a common one
dayjs.extend(utc)
dayjs.extend(timezone)
dayjs.extend(advancedFormat)
dayjs.extend(isToday)
dayjs.extend(relativeTime)
dayjs.extend(duration)
dayjs.extend(localizedFormat)

export default dayjs
