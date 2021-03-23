using System;
using System.Collections.Generic;

namespace Lab3
{ 
    class Program
    {
        public static List<CelestialObject> CreateListOfCelestialObjects()
        {
            string[] lines = System.IO.File.ReadAllLines("celestials.txt");
            List<CelestialObject> celestials = new List<CelestialObject>();

            foreach (string l in lines)
            {
                CelestialObject current;
                if (!CelestialObject.TryParse(l, out current))
                {
                    Console.WriteLine("Wrong input. Please check if the data in file is correct");
                    return null;
                }

                celestials.Add(current);
            }

            Console.WriteLine("File was successfully read. Adding a new object in the list...");
            List<string> newObjElems = new List<string> { "polonium", "lithium", "helium" };
            CelestialObject newObj = new CelestialObject(newObjElems, "Pandora", "Planet", 20, 0.0004, 1E-8,
                                                         CelestialObject.MassFormat.SolarMass,
                                                         CelestialObject.DistanceFormat.LightYear,
                                                         CelestialObject.RadiusFormat.SolarRadius);
            celestials.Add(newObj);
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

        public static int Main(string[] args)
        {
            Console.WriteLine("Creating the list of celestial objects...");
            List<CelestialObject> celestials = CreateListOfCelestialObjects();
            if (celestials == null)
            {
                Console.WriteLine("Error reading celestials.txt");
                return 1;
            }

            Console.WriteLine("Showing all objects:");
            ShowListOfCelestials(celestials);

            Console.WriteLine("Showing only main elements of objects: ");
            ShowMainElementsOfCelestials(celestials);

            Console.WriteLine("Converting all distances to astronomical units, radiuses to kilometers, masses to kilograms:");
            ConvertUnits(celestials);

            return 0;
        }
    }
}
