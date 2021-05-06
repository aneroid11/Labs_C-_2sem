using System;

namespace lab7
{
    static class MathFunctions
    {
        private static void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        public static int GreatestCommonDivisor(int a, int b)
        {
            int absA = Math.Abs(a);
            int absB = Math.Abs(b);

            if (absA < absB)
            {
                Swap(ref absA, ref absB);
            }

            if (absB == 0) { return absA; }

            return GreatestCommonDivisor(absB, absA % absB);
        }

        public static int LeastCommonMultiple(int a, int b)
        {
            int absA = Math.Abs(a);
            int absB = Math.Abs(b);

            if (absA == absB)
            {
                return absA;
            }

            return absA * absB / GreatestCommonDivisor(absA, absB);
        }
    }
}