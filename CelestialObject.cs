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
        public static double GravitationalConstant() { return 6.674E-11; }
        public static double EarthGravity() { return 9.80665; }
    }

    // Базовый класс небесного тела
    class CelestialObject
    {
        public enum MassFormat { SolarMass, Kilograms }
        public enum RadiusFormat { SolarRadius, Kilometers }
        public enum DistanceFormat { Kilometers, Parsec, LightYear, AstronomicalUnit }

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
            MassesFormat = MassFormat.Kilograms;
            DistancesFormat = DistanceFormat.Kilometers;
            RadiusesFormat = RadiusFormat.Kilometers;
            _mainElements = new List<string>();
        }

        public CelestialObject(List<string> mainElements,
                               string name = "",
                               string type = "",
                               double distFromEarth = 0.0,
                               double radius = 0.0,
                               double mass = 0.0,
                               MassFormat massesFormat = MassFormat.Kilograms,
                               DistanceFormat distancesFormat = DistanceFormat.Kilometers,
                               RadiusFormat radiusesFormat = RadiusFormat.Kilometers)
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

        public double EscapeVelocity()
        {
            double massKg = MassesFormat == MassFormat.Kilograms ? Mass : Mass * Astronomy.SunMassKg();
            double radiusM = RadiusesFormat == RadiusFormat.Kilometers ? Radius : Radius * Astronomy.SunRadiusKm();
            radiusM *= 1000.0;

            return Math.Sqrt(2 * Astronomy.GravitationalConstant() * massKg / radiusM);
        }

        public double MeanDensity()
        {
            double massKg = MassesFormat == MassFormat.Kilograms ? Mass : Mass * Astronomy.SunMassKg();
            double radiusM = RadiusesFormat == RadiusFormat.Kilometers ? Radius : Radius * Astronomy.SunRadiusKm();
            radiusM *= 1000.0;
            double volumeM3 = (4.0 / 3.0) * Math.PI * (radiusM * radiusM * radiusM);

            return massKg / volumeM3;
        }

        public double SurfaceGravity()
        {
            double massKg = MassesFormat == MassFormat.Kilograms ? Mass : Mass * Astronomy.SunMassKg();
            double radiusM = RadiusesFormat == RadiusFormat.Kilometers ? Radius : Radius * Astronomy.SunRadiusKm();
            radiusM *= 1000.0;

            return (Astronomy.GravitationalConstant() * massKg) / (radiusM * radiusM);
        }

        virtual public void Convert(MassFormat mf)
        {
            MassFormat old = MassesFormat;
            MassesFormat = mf;

            if (old == mf) { return; }

            if (old == MassFormat.Kilograms && mf == MassFormat.SolarMass)
            {
                Mass /= Astronomy.SunMassKg();
            }
            else if (old == MassFormat.SolarMass && mf == MassFormat.Kilograms)
            {
                Mass *= Astronomy.SunMassKg();
            }
        }

        virtual public void Convert(RadiusFormat rf)
        {
            RadiusFormat old = RadiusesFormat;
            RadiusesFormat = rf;

            if (old == rf) { return; }

            if (old == RadiusFormat.Kilometers && rf == RadiusFormat.SolarRadius)
            {
                Radius /= Astronomy.SunRadiusKm();
            }
            else if (old == RadiusFormat.SolarRadius && rf == RadiusFormat.Kilometers)
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
                case DistanceFormat.AstronomicalUnit:
                    DistanceFromEarth *= Astronomy.KmInAstronomicalUnit();
                    break;
                case DistanceFormat.LightYear:
                    DistanceFromEarth *= Astronomy.KmInLightYear();
                    break;
                case DistanceFormat.Parsec:
                    DistanceFromEarth *= Astronomy.KmInParsek();
                    break;
            }

            // А затем в нужную единицу
            switch (df)
            {
                case DistanceFormat.AstronomicalUnit:
                    DistanceFromEarth /= Astronomy.KmInAstronomicalUnit();
                    break;
                case DistanceFormat.LightYear:
                    DistanceFromEarth /= Astronomy.KmInLightYear();
                    break;
                case DistanceFormat.Parsec:
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

            if (DistancesFormat == DistanceFormat.Kilometers)
            {
                s += DistanceFromEarth.ToString("E") + " kilometers from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.Parsec)
            {
                s += DistanceFromEarth.ToString("E") + " parsecs from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.LightYear)
            {
                s += DistanceFromEarth.ToString("E") + " light years from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.AstronomicalUnit)
            {
                s += DistanceFromEarth.ToString("E") + " astronomical units from Earth\n";
            }

            if (RadiusesFormat == RadiusFormat.Kilometers)
            {
                s += "Radius: " + Radius.ToString("E") + " kilometers\n";
            }
            else if (RadiusesFormat == RadiusFormat.SolarRadius)
            {
                s += "Radius: " + Radius.ToString("E") + " solar radiuses\n";
            }

            if (MassesFormat == MassFormat.Kilograms)
            {
                s += "Mass: " + Mass.ToString("E") + " kilograms\n";
            }
            else if (MassesFormat == MassFormat.SolarMass)
            {
                s += "Mass: " + Mass.ToString("E") + " solar masses\n";
            }

            s += "Mean density: " + MeanDensity().ToString("E") + " kg/m^3\n";
            s += "Surface gravity: " + SurfaceGravity().ToString("E") + " m/s^2\n";
            s += "Escape velocity: " + EscapeVelocity().ToString("E") + " m/s\n";

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
                    massFormat = MassFormat.Kilograms;
                    break;
                case "SM":
                    massFormat = MassFormat.SolarMass;
                    break;
                default:
                    co = new CelestialObject();
                    return false;
            }

            DistanceFormat distanceFormat;
            switch (distFormatStr)
            {
                case "km":
                    distanceFormat = DistanceFormat.Kilometers;
                    break;
                case "pc":
                    distanceFormat = DistanceFormat.Parsec;
                    break;
                case "ly":
                    distanceFormat = DistanceFormat.LightYear;
                    break;
                case "au":
                    distanceFormat = DistanceFormat.AstronomicalUnit;
                    break;
                default:
                    co = new CelestialObject();
                    return false;
            }

            RadiusFormat radiusFormat;
            switch (radiusFormatStr)
            {
                case "km":
                    radiusFormat = RadiusFormat.Kilometers;
                    break;
                case "SR":
                    radiusFormat = RadiusFormat.SolarRadius;
                    break;
                default:
                    co = new CelestialObject();
                    return false;
            }

            co = new CelestialObject(mainElements, name, type, dist, radius, mass, massFormat, distanceFormat, radiusFormat);
            return true;
        }
    }
}
