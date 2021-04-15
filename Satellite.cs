using System;
using System.Collections.Generic;

namespace Sky
{
    public class Satellite : CelestialObject
    {
        public Planet OrbitingAround { get; protected set; }

        public Satellite()
        {
            OrbitingAround = new Planet();
        }

        public Satellite(List<string> mainElements,
                               Planet orbitingAround,
                               string name = "",
                               double distFromEarth = 0.0,
                               double radius = 0.0,
                               double mass = 0.0,
                               MassFormat massesFormat = MassFormat.Kilograms,
                               DistanceFormat distancesFormat = DistanceFormat.Kilometers,
                               RadiusFormat radiusesFormat = RadiusFormat.Kilometers)
            : base(mainElements, name, "satellite", distFromEarth, radius, mass, massesFormat, distancesFormat, radiusesFormat)
        {
            OrbitingAround = orbitingAround;
        }

        public Satellite(Satellite s)
            : base(s._mainElements, s.Name, "satellite", s.DistanceFromEarth, s.Radius, s.Mass, s.MassesFormat, s.DistancesFormat, s.RadiusesFormat)
        {
            OrbitingAround = s.OrbitingAround;
        }

        public static Satellite GenerateRandom(Planet orbitingAround)
        {
            Random random = new Random();

            List<string> someElems = new List<string> { "helium", "hydrogen", "oxygen", "neon", "silicium", "carbon", "ferrum", "aurum" };
            List<string> mainElems = new List<string>();
            int len = random.Next(2, 5);

            for (int i = 0; i < len; i++)
            {
                mainElems.Add(someElems[i]);
            }

            Satellite satellite = new Satellite(mainElems, orbitingAround, Astronomy.GenerateRandomObjectName().ToString(),
                                                random.Next(1, 10), random.Next(20, 60), random.Next(200, 10000), 
                                                MassFormat.Kilograms, DistanceFormat.LightYear, RadiusFormat.Kilometers);

            return satellite;
        }

        public override string ToString()
        {
            string str = base.ToString();
            str += "Is orbiting around " + OrbitingAround.Name;

            return str;
        }

        public override Allegro.Color GetColor()
        {
            return Allegro.MapRGB(100, 100, 100);
        }
    }
}
