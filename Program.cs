/*
 * Предусмотреть необходимый набор:
 * методов, 
 * полей, 
 * свойств, 
 * конструкторов 
 * и индексаторов в реализуемом классе. 
 * Реализовать:
 * статические элементы класса (например, создание уникального Id), 
 * перегрузку методов. 
 * Продемонстрировать работу с созданным классом.
 * */

using System;
using System.Collections.Generic;

namespace Lab3
{ 
    // Общие константы
    static class Astronomy
    {
        public static double SunMassKg() { return 1.989E30; }
        public static double KmInParsek() { return 3.086E13; }
        public static double KmInLightYear() { return 9.461E12; }
        public static double KmInAstronomicalUnit() { return 1.496E8; }
        public static double SunRadiusKm() { return 6.96E5; }
    }

    // Базовый класс небесного тела
    class CelestialObject
    {
        public enum MassFormat { SUN_MASS, KILOGRAMS }
        public enum RadiusFormat { SUN_RADIUS, KILOMETERS }
        public enum DistanceFormat { KILOMETERS, PARSEK, LIGHT_YEAR, ASTRONOMICAL_UNIT }

        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public double DistanceFromEarth { get; protected set; }
        public double Radius { get; protected set; }
        public double Mass { get; protected set; }
        public MassFormat MassesFormat { get; protected set; }
        public DistanceFormat DistancesFormat { get; protected set; }
        public RadiusFormat RadiusesFormat { get; protected set; }

        protected List<string> _mainElements;
        public int NumElements() { return _mainElements.Count; }

        public string this[int index]
        {
            get
            {
                return _mainElements[index];
            }
        }

        public CelestialObject()
        {
            Name = Type = "";
            DistanceFromEarth = Radius = Mass = 0.0;
            MassesFormat = MassFormat.KILOGRAMS;
            DistancesFormat = DistanceFormat.KILOMETERS;
            RadiusesFormat = RadiusFormat.KILOMETERS;
            _mainElements = new List<string>();
        }

        public CelestialObject(List<string> mainElements, 
                               string name = "", 
                               string type = "",
                               double distFromEarth = 0.0,
                               double radius = 0.0,
                               double mass = 0.0, 
                               MassFormat massesFormat = MassFormat.KILOGRAMS, 
                               DistanceFormat distancesFormat = DistanceFormat.KILOMETERS, 
                               RadiusFormat radiusesFormat = RadiusFormat.KILOMETERS)
        {
            Name = name;
            Type = type;
            DistanceFromEarth = distFromEarth;
            Radius = radius;
            Mass = mass;
            _mainElements = mainElements;
            MassesFormat = massesFormat;
            DistancesFormat = distancesFormat;
            RadiusesFormat = radiusesFormat;
        }

        public CelestialObject(CelestialObject co)
        {
            Name = co.Name;
            Type = co.Type;
            DistanceFromEarth = co.DistanceFromEarth;
            Radius = co.Radius;
            Mass = co.Mass;
            _mainElements = co._mainElements;
            MassesFormat = co.MassesFormat;
            DistancesFormat = co.DistancesFormat;
            RadiusesFormat = co.RadiusesFormat;
        }

        virtual public void Convert(MassFormat mf)
        {
            MassFormat old = MassesFormat;
            MassesFormat = mf;

            if (old == mf) { return; }

            if (old == MassFormat.KILOGRAMS && mf == MassFormat.SUN_MASS)
            {
                Mass /= Astronomy.SunMassKg();
            }
            else if (old == MassFormat.SUN_MASS && mf == MassFormat.KILOGRAMS)
            {
                Mass *= Astronomy.SunMassKg();
            }
        }

        virtual public void Convert(RadiusFormat rf)
        {
            RadiusFormat old = RadiusesFormat;
            RadiusesFormat = rf;

            if (old == rf) { return; }

            if (old == RadiusFormat.KILOMETERS && rf == RadiusFormat.SUN_RADIUS)
            {
                Radius /= Astronomy.SunRadiusKm();
            }
            else if (old == RadiusFormat.SUN_RADIUS && rf == RadiusFormat.KILOMETERS)
            {
                Radius *= Astronomy.SunRadiusKm();
            }
        }

        virtual public void Convert(DistanceFormat df)
        {
            DistanceFormat old = DistancesFormat;
            DistancesFormat = df;

            if (old == df) { return; }

            // Переводим сначала всё в километры
            switch (old)
            {
                case DistanceFormat.ASTRONOMICAL_UNIT:
                    DistanceFromEarth *= Astronomy.KmInAstronomicalUnit();
                    break;
                case DistanceFormat.LIGHT_YEAR:
                    DistanceFromEarth *= Astronomy.KmInLightYear();
                    break;
                case DistanceFormat.PARSEK:
                    DistanceFromEarth *= Astronomy.KmInParsek();
                    break;
            }

            // А затем в нужную единицу
            switch (df)
            {
                case DistanceFormat.ASTRONOMICAL_UNIT:
                    DistanceFromEarth /= Astronomy.KmInAstronomicalUnit();
                    break;
                case DistanceFormat.LIGHT_YEAR:
                    DistanceFromEarth /= Astronomy.KmInLightYear();
                    break;
                case DistanceFormat.PARSEK:
                    DistanceFromEarth /= Astronomy.KmInParsek();
                    break;
            }
        }
    
        public override string ToString()
        {
            string s = "";
            s += "Celestial object: ";
            s += Name + "\n";
            s += "Type: " + Type + "\n";

            if (DistancesFormat == DistanceFormat.KILOMETERS)
            {
                s += DistanceFromEarth.ToString("E") + " kilometers from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.PARSEK)
            {
                s += DistanceFromEarth.ToString("E") + " parseks from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.LIGHT_YEAR)
            {
                s += DistanceFromEarth.ToString("E") + " light years from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.ASTRONOMICAL_UNIT)
            {
                s += DistanceFromEarth.ToString("E") + " astronomical units from Earth\n";
            }

            if (RadiusesFormat == RadiusFormat.KILOMETERS)
            {
                s += "Radius: " + Radius.ToString("E") + " kilometers\n";
            }
            else if (RadiusesFormat == RadiusFormat.SUN_RADIUS)
            {
                s += "Radius: " + Radius.ToString("E") + " solar radiuses\n";
            }

            if (MassesFormat == MassFormat.KILOGRAMS) 
            { 
                s += "Mass: " + Mass.ToString("E") + " kilograms\n"; 
            }
            else if (MassesFormat == MassFormat.SUN_MASS) 
            { 
                s += "Mass: " + Mass.ToString("E") + " solar masses\n"; 
            }

            s += "Main elements: ";
            foreach (string el in _mainElements)
            {
                s += el;
                s += " ";
            }
            s += "\n";

            return s;
        }

        public static bool TryParse(string str, out CelestialObject co)
        {
            if (string.IsNullOrEmpty(str))
            {
                co = new CelestialObject();
                return false;
            }

            char[] separators = { ' ', ',', '\t' };
            string[] parts = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 9)
            {
                co = new CelestialObject();
                return false;
            }

            string name = parts[0];
            string type = parts[1];
            string distStr = parts[2];
            string radiusStr = parts[3];
            string massStr = parts[4];
            string massesFormatStr = parts[5];
            string distFormatStr = parts[6];
            string radiusFormatStr = parts[7];

            List<string> mainElements = new List<string>();
            for (int i = 8; i < parts.Length; i++)
            {
                mainElements.Add(parts[i]);
            }

            double dist;
            bool result = double.TryParse(distStr, out dist);
            if (!result || dist < 0.0)
            { 
                co = new CelestialObject();
                return false;
            }

            double radius;
            result = double.TryParse(radiusStr, out radius);
            if (!result || radius < 0.0)
            {
                co = new CelestialObject();
                return false;
            }

            double mass;
            result = double.TryParse(massStr, out mass);
            if (!result || mass < 0.0)
            {
                co = new CelestialObject();
                return false;
            }

            MassFormat massFormat;
            switch (massesFormatStr)
            {
                case "kg":
                    massFormat = MassFormat.KILOGRAMS;
                    break;
                case "SM":
                    massFormat = MassFormat.SUN_MASS;
                    break;
                default:
                    co = new CelestialObject();
                    return false;
            }

            DistanceFormat distanceFormat;
            switch (distFormatStr)
            {
                case "km":
                    distanceFormat = DistanceFormat.KILOMETERS;
                    break;
                case "pc":
                    distanceFormat = DistanceFormat.PARSEK;
                    break;
                case "ly":
                    distanceFormat = DistanceFormat.LIGHT_YEAR;
                    break;
                case "au":
                    distanceFormat = DistanceFormat.ASTRONOMICAL_UNIT;
                    break;
                default:
                    co = new CelestialObject();
                    return false;
            }

            RadiusFormat radiusFormat;
            switch (radiusFormatStr)
            {
                case "km":
                    radiusFormat = RadiusFormat.KILOMETERS;
                    break;
                case "SR":
                    radiusFormat = RadiusFormat.SUN_RADIUS;
                    break;
                default:
                    co = new CelestialObject();
                    return false;
            }

            co = new CelestialObject(mainElements, name, type, dist, radius, mass, massFormat, distanceFormat, radiusFormat);
            return true;
        }
    }

    class Program
    {
        public static int Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("celestials.txt");
            List<CelestialObject> celestials = new List<CelestialObject>();

            foreach (string l in lines)
            {
                CelestialObject current;
                if (!CelestialObject.TryParse(l, out current))
                {
                    Console.WriteLine("Wrong input");
                    return 1;
                }

                celestials.Add(current);
            }

            if (celestials.Count > 0)
            {
                foreach (CelestialObject curr in celestials)
                {
                    Console.WriteLine("\nName: " + curr.Name);
                    for (int i = 0; i < curr.NumElements(); i++)
                    {
                        Console.WriteLine(curr[i]);
                    }
                }
            }

            return 0;
        }
    }
}
