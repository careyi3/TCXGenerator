using System;
using System.Globalization;
using System.Collections.Generic;
using www.garmin.com.xmlschemas.TrainingCenterDatabase.v2;

namespace TCXGenerator
{
    public class TCXFileBuilder
    {
        private string ActivityDate { get; }
        private string ActivityDuration { get; }
        private int Calories { get; }
        private int MaxHR { get; }
        private int AverageHR { get; }

        public TCXFileBuilder(string activityDate, string activityDuration, int calories, int maxHr, int averageHr)
        {
            ActivityDate = activityDate;
            ActivityDuration = activityDuration;
            Calories = calories;
            MaxHR = maxHr;
            AverageHR = averageHr;
        }

        public TrainingCenterDatabase BuildFile()
        {
            var activityDate = ActivityDate;
            var activityDuration = ActivityDuration;
            var calories = Calories;
            var maxHR = MaxHR;
            var averageHR = AverageHR;

            var duration = TimeSpan.ParseExact(activityDuration, @"mm\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.AssumeNegative);
            var date = DateTime.ParseExact(activityDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var minHR = (2 * averageHR) - maxHR;
            var durationInSeconds = duration.TotalSeconds * -1;

            TrainingCenterDatabase trainingCenterDatabase = new TrainingCenterDatabase();
            ActivityList_t activityList = new ActivityList_t();
            Activity_t activity = new Activity_t();
            activity.Sport = Sport_t.Other;
            activity.Id = date;
            ActivityLap_t lap = new ActivityLap_t();

            HeartRateInBeatsPerMinute_t averageHeartRate = new HeartRateInBeatsPerMinute_t();
            averageHeartRate.Value = (byte)averageHR;
            lap.AverageHeartRateBpm = averageHeartRate;

            HeartRateInBeatsPerMinute_t maxHeartRate = new HeartRateInBeatsPerMinute_t();
            maxHeartRate.Value = (byte)maxHR;
            lap.MaximumHeartRateBpm = maxHeartRate;

            lap.Calories = (ushort)calories;

            lap.TotalTimeSeconds = durationInSeconds;

            lap.Intensity = Intensity_t.Active;

            lap.TriggerMethod = TriggerMethod_t.Manual;

            Track_t track = new Track_t();
            var trackPoints = new List<Trackpoint_t>();

            Trackpoint_t trackPoint1 = new Trackpoint_t();
            trackPoint1.Time = date;
            HeartRateInBeatsPerMinute_t heartRate1 = new HeartRateInBeatsPerMinute_t();
            heartRate1.Value = (byte)minHR;
            trackPoint1.HeartRateBpm = heartRate1;
            trackPoints.Add(trackPoint1);

            Trackpoint_t trackPoint2 = new Trackpoint_t();
            trackPoint2.Time = date.AddSeconds(durationInSeconds / 4);
            HeartRateInBeatsPerMinute_t heartRate2 = new HeartRateInBeatsPerMinute_t();
            heartRate2.Value = (byte)averageHR;
            trackPoint2.HeartRateBpm = heartRate2;
            trackPoints.Add(trackPoint2);

            Trackpoint_t trackPoint3 = new Trackpoint_t();
            trackPoint3.Time = date.AddSeconds(2 * (durationInSeconds / 4));
            HeartRateInBeatsPerMinute_t heartRate3 = new HeartRateInBeatsPerMinute_t();
            heartRate3.Value = (byte)averageHR;
            trackPoint3.HeartRateBpm = heartRate3;
            trackPoints.Add(trackPoint3);

            Trackpoint_t trackPoint4 = new Trackpoint_t();
            trackPoint4.Time = date.AddSeconds(3 * (durationInSeconds / 4));
            HeartRateInBeatsPerMinute_t heartRate4 = new HeartRateInBeatsPerMinute_t();
            heartRate4.Value = (byte)averageHR;
            trackPoint4.HeartRateBpm = heartRate4;
            trackPoints.Add(trackPoint4);

            Trackpoint_t trackPoint5 = new Trackpoint_t();
            trackPoint5.Time = date.AddSeconds(durationInSeconds);
            HeartRateInBeatsPerMinute_t heartRate5 = new HeartRateInBeatsPerMinute_t();
            heartRate5.Value = (byte)maxHR;
            trackPoint5.HeartRateBpm = heartRate5;
            trackPoints.Add(trackPoint5);


            track.Trackpoint = trackPoints;
            var tracks = new List<Track_t>();
            tracks.Add(track);
            lap.Track = tracks;

            var laps = new List<ActivityLap_t>();
            laps.Add(lap);
            activity.Lap = laps;
            var activities = new List<Activity_t>();
            activities.Add(activity);
            activityList.Activity = activities;
            trainingCenterDatabase.Activities = activityList;

            return trainingCenterDatabase;
        }
    }
}