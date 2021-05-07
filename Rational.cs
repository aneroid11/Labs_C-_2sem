using System;

namespace lab7
{
    class Rational : IFormattable, IEquatable<Rational>, IComparable<Rational>
    {
        public int Numerator { get; private set; }

        public uint _denominator;
        public uint Denominator
        {
            get
            {
                return _denominator;
            }
            private set
            {
                if (value == 0)
                {
                    throw new DivideByZeroException("Cannot assign 0 to the denominator");
                }
                if (value < 0)
                {
                    throw new ArgumentException("Denominator should be a natural number");
                }

                _denominator = value;
            }
        }

        public Rational(int numerator = 0, uint denominator = 1)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator can't be 0");
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        public Rational(Rational r)
        {
            Numerator = r.Numerator;
            Denominator = r.Denominator;
        }

        public static bool TryParse(string str, out Rational r)
        {
            r = new Rational();

            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            string format = "";
            string[] rWords = str.Split();

            if (rWords.Length > 1)
            {
                format = "W";
            }
            else if (rWords[0][0] == '(')
            {
                format = "B";
            }
            else if (char.IsDigit(rWords[0][0]))
            {
                format = "S";
            }

            if (format == "S" || format == "B")
            {
                if (format == "B")
                {
                    if (str[0] != '(' || str[str.Length - 1] != ')')
                    {
                        return false;
                    }

                    str = str.Remove(0, 1);
                    str = str.Remove(str.Length - 1, 1);
                }

                string[] parts = str.Split('/');

                if (parts.Length != 2)
                {
                    return false;
                }

                int numerator;
                uint denominator;

                if (!int.TryParse(parts[0], out numerator))
                {
                    return false;
                }
                if (!uint.TryParse(parts[1], out denominator))
                {
                    return false;
                }

                r.Numerator = numerator;

                if (denominator == 0) { return false; }
                r.Denominator = denominator;

                return true;
            }
            if (format == "W")
            {
                string[] words = str.Split();

                if (words.Length != 4)
                {
                    return false;
                }

                int numerator;
                uint denominator;

                if (!int.TryParse(words[0], out numerator))
                {
                    return false;
                }
                if (!uint.TryParse(words[3], out denominator))
                {
                    return false;
                }

                if (words[1] != "divided" || words[2] != "by")
                {
                    return false;
                }

                if (denominator == 0) { return false; }
                r.Denominator = denominator;
                r.Numerator = numerator;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string str = Numerator + "/" + Denominator;
            return str;
        }

        public string ToString(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return ToString();
            }

            switch (format)
            {
                case "S":
                    return ToString();
                case "B":
                    return "(" + Numerator + "/" + Denominator + ")";
                case "W":
                    return Numerator + " divided by " + Denominator;
                default:
                    throw new FormatException("Not supported format: " + format);
            }
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format);
        }

        public void Reduce()
        {
            if (Numerator == Denominator)
            {
                Numerator = 1;
                Denominator = 1;
                return;
            }

            int gcd = MathFunctions.GreatestCommonDivisor(Numerator, (int)Denominator);
            Numerator /= gcd;
            Denominator /= (uint)gcd;
        }

        public static void ToCommonDenominator(Rational r1, Rational r2)
        {
            if (r1.Denominator == r2.Denominator) { return; }

            uint cd = (uint)MathFunctions.LeastCommonMultiple((int)r1.Denominator, (int)r2.Denominator);
            uint coef1 = cd / r1.Denominator;
            uint coef2 = cd / r2.Denominator;

            r1.Numerator *= (int)coef1;
            r1.Denominator *= coef1;

            r2.Numerator *= (int)coef2;
            r2.Denominator *= coef2;
        }

        public static Rational operator +(Rational r1)
        {
            return r1;
        }

        public static Rational operator -(Rational r1)
        {
            return new Rational(-r1.Numerator, r1.Denominator);
        }

        public static Rational operator +(Rational r1, Rational r2)
        {
            Rational result = new Rational();
            result.Denominator = (uint)MathFunctions.LeastCommonMultiple((int)r1.Denominator, (int)r2.Denominator);
            result.Numerator = r1.Numerator * ((int)result.Denominator / (int)r1.Denominator) +
                               r2.Numerator * ((int)result.Denominator / (int)r2.Denominator);

            result.Reduce();

            return result;
        }

        public static Rational operator -(Rational r1, Rational r2)
        {
            return r1 + (-r2);
        }

        public static Rational operator *(Rational r1, Rational r2)
        {
            Rational result = new Rational();
            result.Numerator = r1.Numerator * r2.Numerator;
            result.Denominator = r1.Denominator * r2.Denominator;
            result.Reduce();

            return result;
        }

        public static Rational operator /(Rational r1, Rational r2)
        {
            if (r2.Numerator == 0)
            {
                throw new DivideByZeroException();
            }

            Rational factor = new Rational();
            bool negative = r2.Numerator < 0;

            if (negative)
            {
                factor.Denominator = (uint)-r2.Numerator;
            }
            else
            {
                factor.Denominator = (uint)r2.Numerator;
            }

            factor.Numerator = (int)r2.Denominator;
            if (negative) { return -(r1 * factor); }

            return r1 * factor;
        }

        public static bool operator ==(Rational r1, Rational r2)
        {
            r1.Reduce();
            r2.Reduce();

            return r1.Numerator == r2.Numerator && r1.Denominator == r2.Denominator;
        }

        public static bool operator !=(Rational r1, Rational r2)
        {
            return !(r1 == r2);
        }

        public static bool operator >(Rational r1, Rational r2)
        {
            if (r1.Numerator < 0 && r2.Numerator >= 0) { return false; }
            if (r2.Numerator < 0 && r1.Numerator >= 0) { return true; }

            ToCommonDenominator(r1, r2);
            bool greater = r1.Numerator > r2.Numerator;

            r1.Reduce();
            r2.Reduce();
            return greater;
        }

        public static bool operator <(Rational r1, Rational r2)
        {
            return r2 > r1 && r1 != r2;
        }

        public static bool operator >=(Rational r1, Rational r2)
        {
            return (r1 > r2) || (r1 == r2);
        }

        public static bool operator <=(Rational r1, Rational r2)
        {
            return (r1 < r2) || (r1 == r2);
        }

        public bool Equals(Rational other)
        {
            other.Reduce();
            return other == this;
        }

        public override bool Equals(object obj)
        {
            Rational r = obj as Rational;

            if (r == null)
            {
                return false;
            }

            return r == this;
        }

        public override int GetHashCode()
        {
            return Numerator ^ (int)Denominator;
        }

        public int CompareTo(Rational other)
        {
            if (other > this)
            {
                return 1;
            }
            if (other < this)
            {
                return -1;
            }
            return 0;
        }

        public static explicit operator float(Rational r)
        {
            return (float)r.Numerator / r.Denominator;
        }

        public static explicit operator double(Rational r)
        {
            return (double)r.Numerator / r.Denominator;
        }

        public static explicit operator decimal(Rational r)
        {
            return (decimal)r.Numerator / r.Denominator;
        }

        public static explicit operator int(Rational r)
        {
            return r.Numerator / (int)r.Denominator;
        }

        public static explicit operator long(Rational r)
        {
            return r.Numerator / r.Denominator;
        }
    }
}