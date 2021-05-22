using System;

namespace Sky
{
    static class Transformation
    {
        public static double DegToRad(double deg)
        {
            return deg / 180.0 * Math.PI;
        }

        public static double RadToDeg(double rad)
        {
            return rad / Math.PI * 180.0;
        }

        // phi - задаётся в радианах.
        public static void RotatePoint(ref double x, ref double y, double phi)
        {
            double newX = 0.0, newY = 0.0;

            newX = x * Math.Cos(phi) - y * Math.Sin(phi);
            newY = x * Math.Sin(phi) + y * Math.Cos(phi);
            x = newX;
            y = newY;
        }
    }
}
