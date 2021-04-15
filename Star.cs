using System;
using System.Collections.Generic;

namespace Sky
{
    public class Star : CelestialObject
    {
        public char SpectralType { get; protected set; }
        public double Temperature { get; protected set; }

        public Star()
        {
            SpectralType = 'O';
            Temperature = 0.0;
        }

        public Star(List<string> mainElements,
                          string name = "",
                          double distFromEarth = 0.0,
                          double radius = 0.0,
                          double mass = 0.0,
                      MassFormat massesFormat = MassFormat.Kilograms,
                  DistanceFormat distancesFormat = DistanceFormat.Kilometers,
                    RadiusFormat radiusesFormat = RadiusFormat.Kilometers,
                            char spectralType = 'O',
                          double temperature = 0.0) :
            base(mainElements, name, "star", distFromEarth, radius, mass, massesFormat, distancesFormat, radiusesFormat)
        {
            SpectralType = spectralType;
            Temperature = temperature;
        }

        public Star(Star s) : base(s._mainElements, s.Name, s.Type, s.DistanceFromEarth, s.Radius, s.Mass, s.MassesFormat, s.DistancesFormat, s.RadiusesFormat)
        {
            SpectralType = s.SpectralType;
            Temperature = s.Temperature;
        }

        public static Star GenerateRandom()
        {
            Random random = new Random();

            List<string> mainElems = new List<string> { "helium", "hydrogen", "oxygen" };
            string name = Astronomy.GenerateRandomObjectName().ToString();
            char spectralType = ' ';
            int a = random.Next(7);

            switch (a)
            {
                case 0: spectralType = 'O'; break;
                case 1: spectralType = 'G'; break;
                case 2: spectralType = 'B'; break;
                case 3: spectralType = 'F'; break;
                case 4: spectralType = 'K'; break;
                case 5: spectralType = 'M'; break;
                case 6: spectralType = 'A'; break;
            }

            Star star = new Star(mainElems, name, random.Next(1, 10), random.Next(20, 60), random.Next(200, 10000), 
                                 MassFormat.SolarMass, DistanceFormat.LightYear, RadiusFormat.SolarRadius, spectralType, random.Next(5000, 20000));
            star.CalculateXYZ();
            return star;
        }

        public override Allegro.Color GetColor()
        {
            switch (SpectralType)
            {
                case 'O':
                    return Allegro.MapRGB(255, 255, 255);
                case 'B':
                    return Allegro.MapRGB(100, 100, 255);
                case 'A':
                    return Allegro.MapRGB(200, 200, 255);
                case 'F':
                    return Allegro.MapRGB(150, 150, 255);
                case 'G':
                    return Allegro.MapRGB(200, 200, 50);
                case 'K':
                    return Allegro.MapRGB(255, 150, 20);
                case 'M':
                    return Allegro.MapRGB(255, 100, 10);
                default:
                    return Allegro.MapRGB(0, 0, 0);
            }
        }

        public override string ToString()
        {
            string str = base.ToString();
            str += "Spectral type: " + SpectralType;
            str += "\nSurface temperature: " + Temperature + " K";

            return str;
        }
    }
}
