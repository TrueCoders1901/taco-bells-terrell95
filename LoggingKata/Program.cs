using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            string[] lines = File.ReadAllLines(csvPath);

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            // Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the furthest from each other.
            ITrackable location1 = null;
            ITrackable location2 = null;
            // Create a `double` variable to store the distance
            double distance = 0;

            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            for (int i = 0; i < locations.Length; i++)
            {
                ITrackable LocA = locations[i];
                // Create a new corA Coordinate with your locA's lat and long
                GeoCoordinate origin = new GeoCoordinate(LocA.Location.Latitude, LocA.Location.Longitude);

                // Now, do another loop on the locations within the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int j = 0; j < locations.Length; j++)
                {
                    ITrackable LocB = locations[j];
                    // Create a new Coordinate with your locB's lat and long
                    GeoCoordinate destination = new GeoCoordinate(LocB.Location.Latitude, LocB.Location.Longitude);
                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    double length = origin.GetDistanceTo(destination);
                    // If the length is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                    if(length > distance)
                    {
                        distance = length;
                        location1 = LocA;
                        location2 = LocB;
                    }
                }

            }

            // Once you've looped through everything, you've found the two Taco Bells furthest away from each other.
            Console.WriteLine(location1.Name);
            Console.WriteLine(location2.Name);
            Console.WriteLine(distance);
        }
    }
}