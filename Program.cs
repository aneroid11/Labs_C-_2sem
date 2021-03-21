using System;
using System.Collections.Generic;

namespace Lab3
{ 
    class Program
    {
        public static int Main(string[] args)
        {
            // Прочитать все данные об объектах из файла
            string[] lines = System.IO.File.ReadAllLines("celestials.txt");
            List<CelestialObject> celestials = new List<CelestialObject>();

            foreach (string l in lines)
            {
                CelestialObject current;
                if (!CelestialObject.TryParse(l, out current))
                {
                    Console.WriteLine("Wrong input. Please check if the data in file is correct");
                    return 1;
                }

                celestials.Add(current);
            }
            // Создать и добавить в список новый объект
            List<string> newObjElems = new List<string> { "polonium", "lithium", "helium" };
            CelestialObject newObj = new CelestialObject(newObjElems, "Pandora", "Planet", 20, 0.0004, 1E-8,
                                                         CelestialObject.MassFormat.SolarMass,
                                                         CelestialObject.DistanceFormat.LightYear,
                                                         CelestialObject.RadiusFormat.SolarRadius);
            celestials.Add(newObj);
            // Вывести все объекты
            foreach (CelestialObject curr in celestials)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(curr);
            }
            // Вывести только элементы объектов
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
            // Вывести объекты с расстоянием в астрономических единицах, радиусом в 
            // километрах и массой в килограммах
            foreach (CelestialObject curr in celestials)
            {
                Console.WriteLine("----------------------------------------");
                curr.Convert(CelestialObject.DistanceFormat.AstronomicalUnit);
                curr.Convert(CelestialObject.RadiusFormat.Kilometers);
                curr.Convert(CelestialObject.MassFormat.Kilograms);
                Console.WriteLine(curr);
            }

            return 0;
        }
    }
}
