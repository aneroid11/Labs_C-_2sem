using System;
using System.Collections.Generic;

namespace Sky
{
    public class Planet : CelestialObject
    {
        public string OrbitingAround { get; protected set; }
        public string[] Satellites { get; protected set; }

        public Planet()
        {
            OrbitingAround = "";
            Satellites = new string[0];
        }

        public Planet(List<string> mainElements,
                            string orbitingAround = "",
                            string name = "",
                            double distFromEarth = 0.0,
                            double radius = 0.0,
                            double mass = 0.0,
                        MassFormat massesFormat = MassFormat.Kilograms,
                    DistanceFormat distancesFormat = DistanceFormat.Kilometers,
                      RadiusFormat radiusesFormat = RadiusFormat.Kilometers)
            : base(mainElements, name, "planet", distFromEarth, radius, mass, massesFormat, distancesFormat, radiusesFormat)
        {
            OrbitingAround = orbitingAround;
        }

        public Planet(Planet copy)
            : base(copy._mainElements, copy.Name, "planet", copy.DistanceFromEarth, copy.Radius, copy.Mass, copy.MassesFormat, copy.DistancesFormat, copy.RadiusesFormat)
        {
            OrbitingAround = copy.OrbitingAround;
        }

        public override Allegro.Color GetColor()
        {
            return Allegro.MapRGB(150, 50, 20);
        }

        public override string ToString()
        {
            string str = base.ToString();
            str += "Is orbiting around " + OrbitingAround;

            return str;
        }
    }
}
