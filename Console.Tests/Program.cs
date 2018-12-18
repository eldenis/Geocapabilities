using Geocapabilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Console;

namespace Console.Tests
{
    internal class Program
    {
        private static void Main()
        {
            Ex.StartExecution();

            InMemoryTest();
            ReadGoogleGeocodingResultTest();
            ReadGoogleGeocodingResultTest2();
            NoResultReadTest();

            Ex.EndExecution();
        }


        private static void NoResultReadTest()
        {
            Ex.StartTest();

            var result = JsonConvert.DeserializeObject<GeocodingResult>(File.ReadAllText(@"examples\geocoding\google\noresults.json"));

            WriteLine(result.Results.Count > 0 ? "ERROR: There were results found." : "CORRECT: No result was found.");

            Ex.EndTest();
        }


        private static void ReadGoogleGeocodingResultTest()
        {
            Ex.StartTest();

            var mcdo = JsonConvert.DeserializeObject<GeocodingResult>(File.ReadAllText(@"examples\geocoding\google\mcdo.json"));

            var mexicanResult = mcdo?.Results?.FirstOrDefault(x => x.AddressComponents.Any(y => y.LongName == "Ciudad de México"));

            if (mexicanResult == null)
            {
                WriteLine("ERROR: No result was found.");
            }
            else
            {
                var mcdoPoly = new[]
                {
                    new GeoPoint(19.38262,-99.17629), new GeoPoint(19.38375,-99.17698),
                    new GeoPoint(19.38391,-99.17753), new GeoPoint(19.38375,-99.17844),
                    new GeoPoint(19.38337,-99.17947), new GeoPoint(19.38401,-99.18071),
                    new GeoPoint(19.38464,-99.18354), new GeoPoint(19.38407,-99.18393),
                    new GeoPoint(19.37581,-99.18753), new GeoPoint(19.37278,-99.17702),
                    new GeoPoint(19.38071,-99.17453), new GeoPoint(19.38302,-99.17582)
                };

                var noMcdoPoly = new[]
                {
                        new GeoPoint(19.38262, -99.17629), new GeoPoint(19.38385, -99.18404),
                        new GeoPoint(19.3799, -99.17796), new GeoPoint(19.37905, -99.17723),
                        new GeoPoint(19.37662, -99.17878), new GeoPoint(19.37714, -99.17998),
                        new GeoPoint(19.37847, -99.18101), new GeoPoint(19.37982, -99.17801),
                        new GeoPoint(19.38407, -99.18393), new GeoPoint(19.37581, -99.18753),
                        new GeoPoint(19.37355, -99.17904), new GeoPoint(19.38124, -99.17625),
                        new GeoPoint(19.38302, -99.17582)
                    };

                PointInPolygonTest(mcdoPoly, noMcdoPoly, mexicanResult.Geometry.Location);
            }

            Ex.EndTest();
        }

        private static void ReadGoogleGeocodingResultTest2()
        {
            Ex.StartTest();

            var parquehundido = JsonConvert.DeserializeObject<GeocodingResult>(File.ReadAllText(@"examples\geocoding\google\parquehundido.json"));

            var mexicanResult = parquehundido?.Results?.FirstOrDefault(x => x.AddressComponents.Any(y => y.LongName == "Ciudad de México"));

            if (mexicanResult == null)
            {
                WriteLine("ERROR: No result was found.");
            }
            else
            {
                var parquehundidoPoly = new[]
                {
                    new GeoPoint(19.38262,-99.17629), new GeoPoint(19.38375,-99.17698),
                    new GeoPoint(19.38391,-99.17753), new GeoPoint(19.38375,-99.17844),
                    new GeoPoint(19.38337,-99.17947), new GeoPoint(19.38401,-99.18071),
                    new GeoPoint(19.38464,-99.18354), new GeoPoint(19.38407,-99.18393),
                    new GeoPoint(19.37581,-99.18753), new GeoPoint(19.37278,-99.17702),
                    new GeoPoint(19.38071,-99.17453), new GeoPoint(19.38302,-99.17582)
                };

                var noparquehundidoPoly = new[]
                {
                    new GeoPoint(19.38262, -99.17629), new GeoPoint(19.38385, -99.18404),
                    new GeoPoint(19.3799, -99.17796), new GeoPoint(19.37905, -99.17723),
                    new GeoPoint(19.37662, -99.17878), new GeoPoint(19.37714, -99.17998),
                    new GeoPoint(19.37847, -99.18101), new GeoPoint(19.37982, -99.17801),
                    new GeoPoint(19.38407, -99.18393), new GeoPoint(19.37581, -99.18753),
                    new GeoPoint(19.37355, -99.17904), new GeoPoint(19.38124, -99.17625),
                    new GeoPoint(19.38302, -99.17582)
                };

                PointInPolygonTest(parquehundidoPoly, noparquehundidoPoly, mexicanResult.Geometry.Location);
            }

            Ex.EndTest();
        }

        private static void InMemoryTest()
        {
            Ex.StartTest();
            var point = new GeoPoint(19.3806445, -99.1791765);

            var poly = new[]
            {
                new GeoPoint(19.3827, -99.17646), new GeoPoint(19.38181, -99.18007),
                new GeoPoint(19.38415, -99.1841), new GeoPoint(19.3759, -99.1877),
                new GeoPoint(19.37363, -99.17921), new GeoPoint(19.38132, -99.17642),
                new GeoPoint(19.3831, -99.17599)
            };
            var falsePoly = new[]
            {
                new GeoPoint(19.3827, -99.17646), new GeoPoint(19.37881, -99.17822),
                new GeoPoint(19.38415, -99.1841), new GeoPoint(19.3759, -99.1877),
                new GeoPoint(19.37363, -99.17921), new GeoPoint(19.38132, -99.17642),
                new GeoPoint(19.3831, -99.17599)
            };

            PointInPolygonTest(poly, falsePoly, point);

            Ex.EndTest();
        }

        private static void PointInPolygonTest(
            IList<GeoPoint> polygonTrue,
            IList<GeoPoint> polygonFalse,
            GeoPoint point)
        {
            PolygonFence.IsPointInPolygon(polygonTrue, point).Dump("The given point is contained within the polygon, must be True");
            PolygonFence.IsPointInPolygon(polygonFalse, point).Dump("The given point is NOT cotained within the polygon, must be False");
        }
    }

    public static class Ex
    {
        private static Stopwatch TestExecution;
        private static Stopwatch TotalExecution;

        public static void Dump<T>(this T obj, string msg) where T : new()
        {
            WriteLine($"{msg} : {obj}");
        }

        public static void StartTest([CallerMemberName] string testName = null)
        {
            TestExecution = Stopwatch.StartNew();
            WriteLine($"------------------");

            WriteInColor($"{testName}", ConsoleColor.DarkCyan);
            WriteLine($"------------------");

        }

        public static void EndTest([CallerMemberName] string testName = null)
        {
            TestExecution.Stop();
            WriteLine($"------------------");
            WriteInColor($"{testName} ended in {TestExecution.Elapsed.TotalSeconds} secs", ConsoleColor.Cyan);
            WriteLine($"------------------{Environment.NewLine}{Environment.NewLine}");
        }

        public static void StartExecution()
        {
            TotalExecution = Stopwatch.StartNew();
        }

        public static void EndExecution()
        {
            TotalExecution.Stop();
            WriteInColor($"Tests ended in {TotalExecution.Elapsed.TotalSeconds} secs", ConsoleColor.Green);
            WriteInColor("PRESS ANY KEY TO EXIT.", ConsoleColor.Yellow);
            ReadKey();
        }

        private static void WriteInColor(string text, ConsoleColor newColor)
        {
            var color = ForegroundColor;
            ForegroundColor = newColor;
            WriteLine(text);
            ForegroundColor = color;
        }
    }
}
