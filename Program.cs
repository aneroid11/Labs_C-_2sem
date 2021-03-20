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

        public CelestialObject(string name = "", string type = "",
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

            return s;
        }
    }

    class Program
    {
        public static int Main(string[] args)
        {
            CelestialObject example = new CelestialObject("Sun", "Star", 
                                                          150000000.0, 
                                                          Astronomy.SunRadiusKm(), 
                                                          Astronomy.SunMassKg(), 
                                                          CelestialObject.MassFormat.KILOGRAMS, 
                                                          CelestialObject.DistanceFormat.KILOMETERS,
                                                          CelestialObject.RadiusFormat.KILOMETERS);

            Console.WriteLine("Sun with the weight in kg: ");
            Console.WriteLine(example.ToString());
            example.Convert(CelestialObject.MassFormat.SUN_MASS);

            Console.WriteLine("Sun with the weight in sun masses: ");
            Console.WriteLine(example.ToString());

            Console.WriteLine("Sun with the radius in sun radiuses: ");
            example.Convert(CelestialObject.RadiusFormat.SUN_RADIUS);
            Console.WriteLine(example.ToString());

            Console.WriteLine("Distance from Earth in parseks: ");
            example.Convert(CelestialObject.DistanceFormat.PARSEK);
            Console.WriteLine(example.ToString());

            Console.WriteLine("Distance from Earth in light years: ");
            example.Convert(CelestialObject.DistanceFormat.LIGHT_YEAR);
            Console.WriteLine(example.ToString());

            Console.WriteLine("Distance from Earth in astronomical units: ");
            example.Convert(CelestialObject.DistanceFormat.ASTRONOMICAL_UNIT);
            Console.WriteLine(example.ToString());

            Console.WriteLine("Creating celestial object by default: ");
            CelestialObject o = new CelestialObject();
            Console.WriteLine(o.ToString());

            CelestialObject newSun = new CelestialObject();
            newSun = example;
            Console.WriteLine("Now there are two suns: ");
            Console.WriteLine(newSun.ToString());

            return 0;
        }
    }
}
