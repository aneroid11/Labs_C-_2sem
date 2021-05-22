using System;
using System.Collections.Generic;
using System.Text;

namespace Sky
{
    // Базовый класс небесного тела
    public class CelestialObject : IObjectInSpace, IComparable<CelestialObject>, IConvertible
    {
        public enum MassFormat { SolarMass, Kilograms }
        public enum RadiusFormat { SolarRadius, Kilometers }
        public enum DistanceFormat { Kilometers, Parsec, LightYear, AstronomicalUnit }

        // Координаты тела в пространстве. генерируются автоматически на основании 
        // расстояния до тела от Земли
        public double XWorld { get; set; }
        public double YWorld { get; set; }
        public double ZWorld { get; set; }

        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public double DistanceFromEarth { get; protected set; }
        public double Radius { get; set; }
        public double Mass { get; protected set; }
        public MassFormat MassesFormat { get; protected set; }
        public DistanceFormat DistancesFormat { get; protected set; }
        public RadiusFormat RadiusesFormat { get; protected set; }
        public int Id { get; protected set; }

        public ScreenObject Projection { get; protected set; }
        public bool Clicked { get; protected set; }

        public static IntPtr FontForDisplayingInfo { get; set; }

        protected static int _numObjects = 0;

        protected List<string> _mainElements;
        public int NumElements() { return _mainElements.Count; }

        public string this[int index]
        {
            get
            {
                return _mainElements[index];
            }
        }

        public CelestialObject()
        {
            _numObjects++;
            Id = _numObjects;
            Name = Type = "";
            DistanceFromEarth = Radius = Mass = 0.0;
            MassesFormat = MassFormat.Kilograms;
            DistancesFormat = DistanceFormat.Kilometers;
            RadiusesFormat = RadiusFormat.Kilometers;
            _mainElements = new List<string>();
            XWorld = YWorld = ZWorld = 0.0;
            Clicked = false;
        }

        public CelestialObject(List<string> mainElements,
                               string name = "",
                               string type = "",
                               double distFromEarth = 0.0,
                               double radius = 0.0,
                               double mass = 0.0,
                               MassFormat massesFormat = MassFormat.Kilograms,
                               DistanceFormat distancesFormat = DistanceFormat.Kilometers,
                               RadiusFormat radiusesFormat = RadiusFormat.Kilometers)
        {
            _numObjects++;
            Id = _numObjects;
            Name = name;
            Type = type;
            DistanceFromEarth = distFromEarth;
            Radius = radius;
            Mass = mass;
            _mainElements = mainElements;
            MassesFormat = massesFormat;
            DistancesFormat = distancesFormat;
            RadiusesFormat = radiusesFormat;

            Clicked = false;
        }

        public CelestialObject(CelestialObject co)
        {
            _numObjects++;
            Id = _numObjects;
            Name = co.Name;
            Type = co.Type;
            DistanceFromEarth = co.DistanceFromEarth;
            Radius = co.Radius;
            Mass = co.Mass;
            _mainElements = co._mainElements;
            MassesFormat = co.MassesFormat;
            DistancesFormat = co.DistancesFormat;
            RadiusesFormat = co.RadiusesFormat;

            Clicked = false;

            XWorld = co.XWorld;
            YWorld = co.YWorld;
            ZWorld = co.ZWorld;
        }

        public virtual void CalculateXYZ()
        {
            // Найти х, у, z в зависимости от расстояния до Земли
            Random random = new Random();

            DistanceFormat oldDistanceFormat = DistancesFormat;
            ConvertFormat(DistanceFormat.Kilometers);
            double squareDistance = DistanceFromEarth * DistanceFromEarth;
            double avgXYZ = Math.Sqrt(squareDistance / 3);

            do
            {
                XWorld = random.Next(2) == 0 ? random.NextDouble() * avgXYZ * 10 : -random.NextDouble() * avgXYZ * 10;
                ZWorld = random.Next(2) == 0 ? random.NextDouble() * avgXYZ * 10 : -random.NextDouble() * avgXYZ * 10;

                double xWorldSquare = XWorld * XWorld;
                double zWorldSquare = ZWorld * ZWorld;
                double yWorldSquare = squareDistance - xWorldSquare - zWorldSquare;
                YWorld = random.Next() % 2 == 0 ? -Math.Sqrt(yWorldSquare) : Math.Sqrt(yWorldSquare);
            } while (Double.IsNaN(YWorld));

            ConvertFormat(oldDistanceFormat);
        }

        public double EscapeVelocity()
        { 
            double massKg = MassesFormat == MassFormat.Kilograms ? Mass : Mass * Astronomy.SolarMassKg();
            double radiusM = RadiusesFormat == RadiusFormat.Kilometers ? Radius : Radius * Astronomy.SolarRadiusKm();
            radiusM *= 1000.0;

            if (radiusM < Double.Epsilon)
            {
                throw new DivideByZeroException("The radius cannot be zero");
            }

            double velocity = Math.Sqrt(2 * Astronomy.GravitationalConstant() * massKg / radiusM);
            if (velocity > Astronomy.LightSpeedMS())
            {
                return Astronomy.LightSpeedMS();
            }

            return velocity;
        }

        public void RotateAroundY(double phi)
        {
            double newXWorld = XWorld, newZWorld = ZWorld;
            Transformation.RotatePoint(ref newXWorld, ref newZWorld, phi);
            XWorld = newXWorld;
            ZWorld = newZWorld;
        }

        public void RotateAroundX(double phi)
        {
            double newZWorld = ZWorld, newYWorld = YWorld;
            Transformation.RotatePoint(ref newZWorld, ref newYWorld, phi);
            ZWorld = newZWorld;
            YWorld = newYWorld;
        }

        public double MeanDensity()
        {
            double massKg = MassesFormat == MassFormat.Kilograms ? Mass : Mass * Astronomy.SolarMassKg();
            double radiusM = RadiusesFormat == RadiusFormat.Kilometers ? Radius : Radius * Astronomy.SolarRadiusKm();
            radiusM *= 1000.0;
            double volumeM3 = (4.0 / 3.0) * Math.PI * (radiusM * radiusM * radiusM);
            if (volumeM3 < Double.Epsilon)
            {
                throw new DivideByZeroException("The volume cannot be zero");
            }

            return massKg / volumeM3;
        }

        public double SurfaceGravity()
        {
            double massKg = MassesFormat == MassFormat.Kilograms ? Mass : Mass * Astronomy.SolarMassKg();
            double radiusM = RadiusesFormat == RadiusFormat.Kilometers ? Radius : Radius * Astronomy.SolarRadiusKm();
            radiusM *= 1000.0;
            if (radiusM * radiusM < Double.Epsilon)
            {
                throw new DivideByZeroException("The radius cannot be zero");
            }

            return (Astronomy.GravitationalConstant() * massKg) / (radiusM * radiusM);
        }

        virtual public void ConvertFormat(MassFormat mf)
        {
            MassFormat old = MassesFormat;
            MassesFormat = mf;

            if (old == mf) { return; }

            if (old == MassFormat.Kilograms && mf == MassFormat.SolarMass)
            {
                Mass /= Astronomy.SolarMassKg();
            }
            else if (old == MassFormat.SolarMass && mf == MassFormat.Kilograms)
            {
                Mass *= Astronomy.SolarMassKg();
            }
        }

        virtual public void ConvertFormat(RadiusFormat rf)
        {
            RadiusFormat old = RadiusesFormat;
            RadiusesFormat = rf;

            if (old == rf) { return; }

            if (old == RadiusFormat.Kilometers && rf == RadiusFormat.SolarRadius)
            {
                Radius /= Astronomy.SolarRadiusKm();
            }
            else if (old == RadiusFormat.SolarRadius && rf == RadiusFormat.Kilometers)
            {
                Radius *= Astronomy.SolarRadiusKm();
            }
        }

        virtual public void ConvertFormat(DistanceFormat df)
        {
            DistanceFormat old = DistancesFormat;
            DistancesFormat = df;

            if (old == df) { return; }

            // Переводим сначала всё в километры
            switch (old)
            {
                case DistanceFormat.AstronomicalUnit:
                    DistanceFromEarth *= Astronomy.KmInAstronomicalUnit();
                    break;
                case DistanceFormat.LightYear:
                    DistanceFromEarth *= Astronomy.KmInLightYear();
                    break;
                case DistanceFormat.Parsec:
                    DistanceFromEarth *= Astronomy.KmInParsek();
                    break;
            }

            // А затем в нужную единицу
            switch (df)
            {
                case DistanceFormat.AstronomicalUnit:
                    DistanceFromEarth /= Astronomy.KmInAstronomicalUnit();
                    break;
                case DistanceFormat.LightYear:
                    DistanceFromEarth /= Astronomy.KmInLightYear();
                    break;
                case DistanceFormat.Parsec:
                    DistanceFromEarth /= Astronomy.KmInParsek();
                    break;
            }
        }

        public override string ToString()
        {
            string s = "";
            s += "Id: " + Id + "\n";
            s += "Celestial object: ";
            s += Name + "\n";
            s += "Type: " + Type + "\n";

            if (DistancesFormat == DistanceFormat.Kilometers)
            {
                s += DistanceFromEarth.ToString("E") + " kilometers from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.Parsec)
            {
                s += DistanceFromEarth.ToString("E") + " parsecs from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.LightYear)
            {
                s += DistanceFromEarth.ToString("E") + " light years from Earth\n";
            }
            else if (DistancesFormat == DistanceFormat.AstronomicalUnit)
            {
                s += DistanceFromEarth.ToString("E") + " astronomical units from Earth\n";
            }

            if (RadiusesFormat == RadiusFormat.Kilometers)
            {
                s += "Radius: " + Radius.ToString("E") + " kilometers\n";
            }
            else if (RadiusesFormat == RadiusFormat.SolarRadius)
            {
                s += "Radius: " + Radius.ToString("E") + " solar radiuses\n";
            }

            if (MassesFormat == MassFormat.Kilograms)
            {
                s += "Mass: " + Mass.ToString("E") + " kilograms\n";
            }
            else if (MassesFormat == MassFormat.SolarMass)
            {
                s += "Mass: " + Mass.ToString("E") + " solar masses\n";
            }

            try
            {
                s += "Mean density: " + MeanDensity().ToString("E") + " kg/m^3\n";
                s += "Surface gravity: " + SurfaceGravity().ToString("E") + " m/s^2\n";
                s += "Escape velocity: " + EscapeVelocity().ToString("E") + " m/s\n";
            }
            catch (DivideByZeroException exc)
            {
                Console.WriteLine(exc.Message);
            }

            s += "Main elements: ";
            foreach (string el in _mainElements)
            {
                s += el;
                s += " ";
            }
            s += "\n";

            return s;
        }

        public virtual Allegro.Color GetColor()
        {
            return Allegro.MapRGB(255, 255, 255);
        }

        public void PrintInfo(IntPtr font)
        {
            Allegro.DrawMultilineText(font, Allegro.MapRGB(255, 255, 255), 20, 20, 400, 15, 0, ToString());
        }

        public void CreateProjection(double camAngleA, double camAngleB)
        {
            RotateAroundY(camAngleA);
            RotateAroundX(camAngleB);

            Projection = new ScreenObject(this);

            RotateAroundX(-camAngleB);
            RotateAroundY(-camAngleA);
        }

        public virtual void Update(Allegro.MouseState mouse, double camAngleA, double camAngleB)
        {
            CreateProjection(camAngleA, camAngleB);

            int mx = mouse.x;
            int my = mouse.y;
            bool buttonPressed = (mouse.buttons & 1) != 0;

            if (!buttonPressed)
            {
                return;
            }

            double dist = Planetarium.MainClass.GetDistance(mx, my, Projection.X, Projection.Y);
            if (dist <= Projection.Radius)
            {
                Clicked = true;
            }
            else
            {
                Clicked = false;
            }
        }

        public virtual void Draw()
        {
            Projection.Draw();

            if (Clicked)
            {
                PrintInfo(FontForDisplayingInfo);
                Allegro.DrawCircle(Projection.X, Projection.Y, Projection.Radius + 8, Allegro.MapRGB(200, 200, 0), 2.0f);
            }
        }

        public int CompareTo(CelestialObject obj)
        {
            // По умолчанию сравниваем по расстоянию до Земли
            DistanceFormat oldFormat = obj.DistancesFormat;
            obj.ConvertFormat(DistanceFormat.Kilometers);

            double delta = DistanceFromEarth - obj.DistanceFromEarth;

            obj.ConvertFormat(oldFormat);

            if (Math.Abs(delta) <= Double.Epsilon)
            {
                return 0;
            }
            if (delta < 0.0)
            {
                return 1;
            }

            return -1;
        }

        Int16 IConvertible.ToInt16(IFormatProvider provider) { throw new NotSupportedException(); }
        Int32 IConvertible.ToInt32(IFormatProvider provider) { throw new NotSupportedException(); }
        Int64 IConvertible.ToInt64(IFormatProvider provider) { throw new NotSupportedException(); }
        UInt16 IConvertible.ToUInt16(IFormatProvider provider) { throw new NotSupportedException(); }
        UInt32 IConvertible.ToUInt32(IFormatProvider provider) { throw new NotSupportedException(); }
        UInt64 IConvertible.ToUInt64(IFormatProvider provider) { throw new NotSupportedException(); }
        Decimal IConvertible.ToDecimal(IFormatProvider provider) { throw new NotSupportedException(); }
        Single IConvertible.ToSingle(IFormatProvider provider) { throw new NotSupportedException(); }
        SByte IConvertible.ToSByte(IFormatProvider provider) { throw new NotSupportedException(); }
        double IConvertible.ToDouble(IFormatProvider provider) { throw new NotSupportedException(); }
        DateTime IConvertible.ToDateTime(IFormatProvider provider) { throw new NotSupportedException(); }
        char IConvertible.ToChar(IFormatProvider provider) { throw new NotSupportedException(); }
        byte IConvertible.ToByte(IFormatProvider provider) { throw new NotSupportedException(); }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) 
        { 
            if (conversionType == typeof(string))
            {
                return Convert.ToString(provider);
            }
            if (conversionType == typeof(bool))
            {
                return Convert.ToBoolean(provider);
            }

            return null;
        }

        TypeCode IConvertible.GetTypeCode()
        {  
            return TypeCode.Object; 
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            if (Math.Abs(Radius) <= Double.Epsilon)
            {
                return false;
            }
            return true;
        }
    }
}
