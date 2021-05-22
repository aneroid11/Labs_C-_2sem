using System;
using System.Collections.Generic;
using System.Numerics;

namespace Sky
{
    public class Planet : CelestialObject, IRotatingObject, IConvertible
    {
        public Star OrbitingAround { get; protected set; }
        public Vector3 RotatingAxis { get; protected set; }

        public Planet()
        {
            OrbitingAround = new Star();
            RotatingAxis = new Vector3(0, 0, 0);
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
            RotatingAxis = new Vector3(0, 0, 0);
        }

        public Planet(Planet copy)
            : base(copy._mainElements, copy.Name, "planet", copy.DistanceFromEarth, copy.Radius, copy.Mass, copy.MassesFormat, copy.DistancesFormat, copy.RadiusesFormat)
        {
            OrbitingAround = copy.OrbitingAround;
            RotatingAxis = copy.RotatingAxis;
        }

        public override void CalculateXYZ()
        {
            Random random = new Random();
            XWorld = OrbitingAround.XWorld;
            YWorld = OrbitingAround.YWorld;
            ZWorld = OrbitingAround.ZWorld + (random.NextDouble() + 1.8) * 1.2E12;
          
            Vector3 vector = new Vector3((float)XWorld, (float)YWorld, (float)ZWorld);

            DistanceFormat old = DistancesFormat;
            ConvertFormat(DistanceFormat.Kilometers);
            DistanceFromEarth = vector.Length();
            ConvertFormat(old);
        }

        public void CalculateRotatingAxis()
        {
            Vector3 delta = new Vector3((float)(XWorld - OrbitingAround.XWorld), (float)(YWorld - OrbitingAround.YWorld), (float)(ZWorld - OrbitingAround.ZWorld));

            // Нормализовать дельту
            delta = delta / delta.Length();

            // Найти перпендикулярный ей вектор
            Vector3 perp = new Vector3(1, 1, 0);
            perp.Z = -(delta.X + delta.Y) / delta.Z;

            // Нормализовать его
            perp = perp / perp.Length();
            RotatingAxis = perp;
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
            planet.CalculateXYZ();
            planet.CalculateRotatingAxis();

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

        public void MoveAlong()
        {
        }

        public void RotateAround()
        {
            // Вращать вокруг звезды.
            Vector3 delta = new Vector3((float)(XWorld - OrbitingAround.XWorld), (float)(YWorld - OrbitingAround.YWorld), (float)(ZWorld - OrbitingAround.ZWorld));
            Vector3 rotatedDelta = new Vector3(0, 0, 0);

            float sn = (float)Math.Sin(Transformation.DegToRad(0.1));
            float cs = (float)Math.Cos(Transformation.DegToRad(0.1));

            rotatedDelta += delta * cs;
            rotatedDelta += Vector3.Cross(RotatingAxis, delta) * sn;
            rotatedDelta += RotatingAxis * (RotatingAxis * delta) * (1 - cs);

            if (rotatedDelta.Length() > delta.Length())
            {
                rotatedDelta *= (delta.Length() / rotatedDelta.Length());
            }

            XWorld = rotatedDelta.X + OrbitingAround.XWorld;
            YWorld = rotatedDelta.Y + OrbitingAround.YWorld;
            ZWorld = rotatedDelta.Z + OrbitingAround.ZWorld;
        }

        public override void Update(Allegro.MouseState mouse, double camAngleA, double camAngleB)
        {
            base.Update(mouse, camAngleA, camAngleB);

            MoveAlong();
            RotateAround();

            Vector3 coord = new Vector3((float)XWorld, (float)YWorld, (float)ZWorld);
            DistanceFromEarth = coord.Length();
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }
    }
}
