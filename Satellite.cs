using System;
using System.Collections.Generic;

namespace Sky
{
    public class Satellite : CelestialObject
    {
        public string OrbitingAround { get; protected set; }

        public Satellite()
        {
            OrbitingAround = "";
        }

        public Satellite(List<string> mainElements,
                               string name = "",
                               double distFromEarth = 0.0,
                               double radius = 0.0,
                               double mass = 0.0,
                               MassFormat massesFormat = MassFormat.Kilograms,
                               DistanceFormat distancesFormat = DistanceFormat.Kilometers,
                               RadiusFormat radiusesFormat = RadiusFormat.Kilometers,
                               string orbitingAround = "")
            : base(mainElements, name, "satellite", distFromEarth, radius, mass, massesFormat, distancesFormat, radiusesFormat)
        {
            OrbitingAround = orbitingAround;
        }

        public Satellite(Satellite s)
            : base(s._mainElements, s.Name, "satellite", s.DistanceFromEarth, s.Radius, s.Mass, s.MassesFormat, s.DistancesFormat, s.RadiusesFormat)
        {
            OrbitingAround = s.OrbitingAround;
        }

        public override string ToString()
        {
            //Console.WriteLine("fokodskfod");

            string str = base.ToString();
            str += "\nIs orbiting around " + OrbitingAround;

            return str;
        }

        public override Allegro.Color GetColor()
        {
            return Allegro.MapRGB(100, 100, 100);
        }
    }
}
