using System;
using System.Text;

namespace Sky
{
    // Общие константы и методы для астрономических объектов
    static class Astronomy
    {
        public static double SolarMassKg() { return 1.989E30; }
        public static double KmInParsek() { return 3.086E13; }
        public static double KmInLightYear() { return 9.461E12; }
        public static double KmInAstronomicalUnit() { return 1.496E8; }
        public static double SolarRadiusKm() { return 6.96E5; }
        public static double GravitationalConstant() { return 6.674E-11; }
        public static double EarthGravity() { return 9.80665; }
        public static double LightSpeedMS() { return 299792458; }

        public static StringBuilder GenerateRandomObjectName()
        {
            Random random = new Random();
            StringBuilder name = new StringBuilder();
            int len = random.Next(20) + 10;
            for (int i = 0; i < len; i++)
            {
                name.Append((char)random.Next('A', 'Z'));
            }

            return name;
        }
    }
}
