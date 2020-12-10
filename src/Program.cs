using System;
using System.IO;
using System.Linq;
using System.Globalization;
using McMaster.Extensions.CommandLineUtils;
using TCXGenerator.Validators;

namespace TCXGenerator
{
    class Program
    {
        public static int Main(string[] args)
        {
            var rootDir = Path.GetDirectoryName(System.Reflection.Assembly.
GetExecutingAssembly().Location);
            var version = File.ReadAllText($"{rootDir}/version");
            var app = new CommandLineApplication();
            app.Name = "TCXGenerator";
            app.Description = $"Generate .tcx files from basic activity level information.\nVersion: {version}";
            app.HelpOption("-?| -h| --help");
            var activityDate = app.Option<string>("-da|--date", "Activity Date ('yyyy-MM-dd HH:mm:ss')", CommandOptionType.SingleValue).IsRequired();
            activityDate.Validators.Add(new DateTimeValidator());
            var activityDuration = app.Option<string>("-du|--duration", "Activity Duration ('hh:mm')", CommandOptionType.SingleValue).IsRequired();
            activityDuration.Validators.Add(new DurationValidator());
            var calories = app.Option<int>("-c|--calories", "Calories Expended", CommandOptionType.SingleValue).IsRequired();
            var maxHR = app.Option<int>("-mhr|--maxhr", "Max Heart Rate (BPM)", CommandOptionType.SingleValue).IsRequired();
            var averageHR = app.Option<int>("-ahr|--avghr", "Average Heart Rate (BPM)", CommandOptionType.SingleValue).IsRequired();
            var output = app.Option<string>("-o|--output", "Output File Path", CommandOptionType.SingleValue);
            var verbose = app.Option<int>("-v|--verbose", "Output full stack trace for failures.", CommandOptionType.NoValue);

            app.OnValidationError(r =>
            {
                if (app.GetOptions().All(o => !o.HasValue()))
                {
                    app.ShowHelp();
                }
                else
                {
                    Console.Error.WriteLine(r.ErrorMessage);
                }
            });

            app.OnExecute(() =>
            {
                try
                {
                    var builder = new TCXFileBuilder(activityDate.ParsedValue, activityDuration.ParsedValue, calories.ParsedValue, maxHR.ParsedValue, averageHR.ParsedValue);

                    var trainingCenterDatabase = builder.BuildFile();

                    if (output.HasValue())
                    {
                        trainingCenterDatabase.Save(output.ParsedValue);
                    }
                    else
                    {
                        var date = DateTime.ParseExact(activityDate.ParsedValue, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        trainingCenterDatabase.Save($"{((DateTimeOffset)date).ToUnixTimeSeconds()}.tcx");
                    }

                    return 0;
                }
                catch (Exception e)
                {
                    if (verbose.HasValue())
                    {
                        Console.Error.WriteLine("An error occurred.");
                        Console.Error.WriteLine(e);
                        return 1;
                    }
                    else
                    {
                        Console.Error.WriteLine("An error occurred.");
                        return 1;
                    }
                }
            });

            try
            {
                return app.Execute(args);
            }
            catch (Exception e)
            {
                if (verbose.HasValue())
                {
                    Console.Error.WriteLine("An error occurred.");
                    Console.Error.WriteLine(e);
                    return 1;
                }
                else
                {
                    Console.Error.WriteLine("An error occurred.");
                    return 1;
                }
            }
        }
    }
}
