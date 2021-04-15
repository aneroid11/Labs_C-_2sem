using System;
using System.Collections.Generic;

namespace Sky
{
    public class Planet : CelestialObject
    {
        public Star OrbitingAround { get; protected set; }

        public Planet()
        {
            OrbitingAround = new Star();
        }

        public Planet(List<string> mainElements,
                              Star orbitingAround,
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

        public static Planet GenerateRandom(Star orbitingAround)
        {
            Random random = new Random();

            List<string> someElems = new List<string> { "helium", "hydrogen", "oxygen", "neon", "silicium", "carbon", "ferrum", "aurum" };
            List<string> mainElems = new List<string>();
            int len = random.Next(2, 5); 

            for (int i = 0; i < len; i++)
            {
                mainElems.Add(someElems[i]);
            }

            Planet planet = new Planet(mainElems, orbitingAround, Astronomy.GenerateRandomObjectName().ToString(), random.Next(1, 10), random.Next(20, 60), random.Next(200, 10000),
                                       MassFormat.SolarMass, DistanceFormat.LightYear, RadiusFormat.SolarRadius);
            return planet;
        }

        public override Allegro.Color GetColor()
        {
            return Allegro.MapRGB(150, 50, 20);
        }

        public override string ToString()
        {
            string str = base.ToString();
            str += "Is orbiting around " + OrbitingAround.Name;

            return str;
        }
    }
}
