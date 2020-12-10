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
  -du|--duration  Activity Duration ('hh:mm')
  -c|--calories   Calories Expended
  -mhr|--maxhr    Max Heart Rate (BPM)
  -ahr|--avghr    Average Heart Rate (BPM)
  -o|--output     Output File Path
  -v|--verbose    Output full stack trace for failures.
```

```bash
$ TCXGenerator -da "2020-12-10 13:00:00" -du "18:23" -c 400 -mhr 187 -ahr 165
```
