using System;
using System.Text;
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
