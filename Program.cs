using System;
using System.Collections.Generic;

namespace Lab3
{ 
    class Program
    {
        public static CelestialObject GetObjectFromConsole()
        {
            Console.WriteLine("\nEnter the object in the format:");
            Console.WriteLine("[name], [type], [distance from Earth], [radius], [mass], [distance format - km/au/pc/ly], [radius format - SR/km], [mass format - kg/SM], [main elements]");
            Console.WriteLine("For example:");
            Console.WriteLine("Sun, star, 150000000, 1, 1, km, SR, SM, helium, hydrogen");
            string objStr = Console.ReadLine();
            CelestialObject celestial;

            while (!CelestialObject.TryParse(objStr, out celestial))
            {
                Console.WriteLine("Please enter the object correctly");
                objStr = Console.ReadLine();
            }

            return celestial;
        }

        public static List<CelestialObject> CreateListOfCelestialObjects()
        {
            List<CelestialObject> celestials = new List<CelestialObject>();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Press a to add a celestial object, press q to stop adding objects");
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                while (keyInfo.Key != ConsoleKey.A && keyInfo.Key != ConsoleKey.Q)
                {
                    Console.WriteLine("\nInput error. Please press a or q");
                    keyInfo = Console.ReadKey();
                }

                if (keyInfo.Key == ConsoleKey.A)
                {
                    CelestialObject newObj = GetObjectFromConsole();
                    celestials.Add(newObj);
                }
                else if (keyInfo.Key == ConsoleKey.Q && celestials.Count == 0)
                {
                    Console.WriteLine("\nYou didn't enter any objects. Enter something");
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    exit = true;
                }
            }

            Console.WriteLine();
            return celestials;
        }

        public static void ShowListOfCelestials(List<CelestialObject> celestials)
        {
            foreach (CelestialObject curr in celestials)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(curr);
            }

            Console.WriteLine("----------------------------------------");
        }

        public static void ShowMainElementsOfCelestials(List<CelestialObject> celestials)
        {
            foreach (CelestialObject curr in celestials)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(curr.Name);
                for (int i = 0; i < curr.NumElements(); i++)
                {
                    Console.Write(curr[i] + " ");
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("----------------------------------------");
        }

        public static void ConvertUnits(List<CelestialObject> celestials)
        {
            foreach (CelestialObject curr in celestials)
            {
                Console.WriteLine("----------------------------------------");
                curr.Convert(CelestialObject.DistanceFormat.AstronomicalUnit);
                curr.Convert(CelestialObject.RadiusFormat.Kilometers);
                curr.Convert(CelestialObject.MassFormat.Kilograms);
                Console.WriteLine(curr);
            }
            Console.WriteLine("----------------------------------------");
        }

        public static void ShowCelestialWithMaximalGravity(List<CelestialObject> celestials)
        {
            CelestialObject max = celestials[0];
            foreach (CelestialObject curr in celestials)
            {
                if (max.SurfaceGravity() < curr.SurfaceGravity())
                {
                    max = curr;
                }
            }

            Console.WriteLine(max);
        }

        public static int Main(string[] args)
        {
            List<CelestialObject> celestials = CreateListOfCelestialObjects();

            Console.WriteLine("Showing all objects:");
            ShowListOfCelestials(celestials);
            Console.WriteLine("Press any key");
            Console.ReadKey();

            Console.WriteLine("\nShowing only main elements of objects: ");
            ShowMainElementsOfCelestials(celestials);
            Console.WriteLine("Press any key");
            Console.ReadKey();

            Console.WriteLine("\nConverting all distances to astronomical units, radiuses to kilometers, masses to kilograms:");
            ConvertUnits(celestials);
            Console.WriteLine("Press any key");
            Console.ReadKey();

            Console.WriteLine("\nObject with maximal surface gravity:");
            ShowCelestialWithMaximalGravity(celestials);

            return 0;
        }
    }
}
