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
            // Done:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            string[] lines = File.ReadAllLines(csvPath);
            // Log and error if you get 0 lines and a warning if you get 1 line
            if (lines.Length == 0)
            {
                logger.LogError("No input");
            }

            if (lines.Length == 1)
            {
                logger.LogWarning("One line of input");
            }
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            ITrackable[] locations = lines.Select(line => parser.Parse(line)).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // Done: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            double tacoBellDistance = 0;
            double distanceInMiles = 0;
            distanceInMiles = tacoBellDistance / 1609.344;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------

            for (int i = 0; i < locations.Length; i++)
            {
                // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
                var locA = locations[i];

                // Create a new corA Coordinate with your locA's lat and long
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int b = 0; b < locations.Length; b++)
                {
                    // Create a new Coordinate with your locB's lat and long
                    var locB = locations[b];
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;


                    if (corA.GetDistanceTo(corB) > tacoBellDistance)
                    {
                        tacoBellDistance = corA.GetDistanceTo(corB);
                        distanceInMiles = tacoBellDistance / 1609.344;
                        distanceInMiles = Math.Round(distanceInMiles, 2);

                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }



                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

                    // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.


                }


            }


            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are {distanceInMiles} miles apart");




        }
    }
}