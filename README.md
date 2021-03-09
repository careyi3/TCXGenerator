# TCXGenerator

This is a simple console utility that generates basic `.tcx` files for uploading to Strava.

The tool allows you to specify date, time, duration, calories burned, max heart rate and average heart rate. These values will all be encoded into a valid `.tcx` files which can then be uploaded to Strava.

I built this as a personal tool to import non-GPS activities from Fitbit as the exported `.tcx` files from Fitbit are incomplete and don't work with Strava and the native integration between Fitbit and Strava only syncs GPS activities.

## Usage

```bash
$ TCXGenerator
Generate .tcx files from basic activity level information.
Version: edd8eedbf18db3e88e4531165861fa01


Usage: TCXGenerator [options]

Options:
  -?|-h|--help    Show help information
  -da|--date      Activity Date ('yyyy-MM-dd HH:mm:ss')
  -du|--duration  Activity Duration ('mm:ss')
  -c|--calories   Calories Expended
  -mhr|--maxhr    Max Heart Rate (BPM)
  -ahr|--avghr    Average Heart Rate (BPM)
  -o|--output     Output File Path
  -v|--verbose    Output full stack trace for failures.
```

Input:

```bash
$ TCXGenerator -da "2020-12-10 13:00:00" -du "18:23" -c 400 -mhr 187 -ahr 165
```

Output File:

```xml
<?xml version="1.0" encoding="utf-8"?>
<TrainingCenterDatabase xmlns="http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2">
  <Activities>
    <Activity Sport="Other">
      <Id>2020-12-10T13:00:00</Id>
      <Lap>
        <TotalTimeSeconds>1103</TotalTimeSeconds>
        <Calories>400</Calories>
        <AverageHeartRateBpm>
          <Value>165</Value>
        </AverageHeartRateBpm>
        <MaximumHeartRateBpm>
          <Value>187</Value>
        </MaximumHeartRateBpm>
        <Intensity>Active</Intensity>
        <TriggerMethod>Manual</TriggerMethod>
        <Track>
          <Trackpoint>
            <Time>2020-12-10T13:00:00</Time>
            <HeartRateBpm>
              <Value>143</Value>
            </HeartRateBpm>
          </Trackpoint>
          <Trackpoint>
            <Time>2020-12-10T13:04:35.75</Time>
            <HeartRateBpm>
              <Value>165</Value>
            </HeartRateBpm>
          </Trackpoint>
          <Trackpoint>
            <Time>2020-12-10T13:09:11.5</Time>
            <HeartRateBpm>
              <Value>165</Value>
            </HeartRateBpm>
          </Trackpoint>
          <Trackpoint>
            <Time>2020-12-10T13:13:47.25</Time>
            <HeartRateBpm>
              <Value>165</Value>
            </HeartRateBpm>
          </Trackpoint>
          <Trackpoint>
            <Time>2020-12-10T13:18:23</Time>
            <HeartRateBpm>
              <Value>187</Value>
            </HeartRateBpm>
          </Trackpoint>
        </Track>
      </Lap>
    </Activity>
  </Activities>
</TrainingCenterDatabase>
```
