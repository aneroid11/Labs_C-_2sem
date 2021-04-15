﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace Sky
{
    public class Satellite : CelestialObject
    {
        public Planet OrbitingAround { get; protected set; }
        public Vector3 RotatingAxis { get; protected set; }

        public Satellite()
        {
            OrbitingAround = new Planet();
            RotatingAxis = new Vector3(0, 0, 0);
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
            RotatingAxis = new Vector3(0, 0, 0);
        }

        public Satellite(Satellite s)
            : base(s._mainElements, s.Name, "satellite", s.DistanceFromEarth, s.Radius, s.Mass, s.MassesFormat, s.DistancesFormat, s.RadiusesFormat)
        {
            OrbitingAround = s.OrbitingAround;
            RotatingAxis = s.RotatingAxis;
        }

        public override void CalculateXYZ()
        {
            Random random = new Random();
            XWorld = OrbitingAround.XWorld;
            YWorld = OrbitingAround.YWorld;
            ZWorld = OrbitingAround.ZWorld + (random.NextDouble() + 0.5) * 1.2E12;

            Vector3 vector = new Vector3((float)XWorld, (float)YWorld, (float)ZWorld);

            DistanceFormat old = DistancesFormat;
            Convert(DistanceFormat.Kilometers);
            DistanceFromEarth = vector.Length();
            Convert(old);
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
            satellite.CalculateXYZ();
            satellite.CalculateRotatingAxis();
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

        public override void Update(Allegro.MouseState mouse, double camAngleA, double camAngleB)
        {
            base.Update(mouse, camAngleA, camAngleB);

            // Вращать вокруг планеты.

            Vector3 delta = new Vector3((float)(XWorld - OrbitingAround.XWorld), (float)(YWorld - OrbitingAround.YWorld), (float)(ZWorld - OrbitingAround.ZWorld));
            Vector3 rotatedDelta = new Vector3(0, 0, 0);

            float sn = (float)Math.Sin(Transformation.DegToRad(1.0));
            float cs = (float)Math.Cos(Transformation.DegToRad(1.0));
             
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
    }
}
