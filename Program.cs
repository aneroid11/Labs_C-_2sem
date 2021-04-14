using System;
using System.Collections.Generic;
using Sky;

namespace Lab5
{
    class MainClass
    {
        public const int screenWidth = 1024;
        public const int screenHeight = 768;
        public const double screenWidthInKm = 0.0004;
        public const double screenHeightInKm = 0.0003;
        public const double kmInPixel = screenWidthInKm / screenWidth;
        //public const double projectionCoef = 0.0005;
        public const double projectionCoef = 0.001;

        public struct ScreenObject
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Radius { get; set; }
            public bool Visible { get; private set; }
            public Allegro.Color Color { get; private set; }

            public ScreenObject(int x, int y, int radius, Allegro.Color cl)
            {
                X = x;
                Y = y;
                Radius = radius;
                Visible = true;
                Color = cl;
            }

            public ScreenObject(CelestialObject celestial)
            {
                if (celestial.ZWorld <= 0.0)
                {
                    X = Y = Radius = 0;
                    Visible = false;
                    Color = Allegro.MapRGB(0, 0, 0);
                    return;
                }

                double xScreen = celestial.XWorld * projectionCoef / celestial.ZWorld + screenWidthInKm / 2;
                double yScreen = celestial.YWorld * projectionCoef / celestial.ZWorld + screenHeightInKm / 2;

                double celRadInKm = 0.0;
                CelestialObject.RadiusFormat oldFormat = celestial.RadiusesFormat;
                celestial.Convert(CelestialObject.RadiusFormat.Kilometers);
                celRadInKm = celestial.Radius;
                celestial.Convert(oldFormat);

                double radiusProjection = celRadInKm * projectionCoef / celestial.ZWorld;

                X = KmToPixels(xScreen);
                Y = KmToPixels(yScreen);
                Radius = KmToPixels(radiusProjection);
                Visible = true;

                if (Radius < 1)
                {
                    // Если объект слишком далеко, то проекция вырождается практически в точку
                    Radius = 2;
                }

                Color = celestial.GetColor();
            }

            public void Draw()
            {
                if (!Visible) { return; }

                Allegro.DrawFilledCircle(X, Y, Radius, Color);
            }

            public static double PixelsToKm(int pixels)
            {
                return pixels * kmInPixel;
            }

            public static int KmToPixels(double km)
            {
                return (int)(km / kmInPixel);
            }
        }

        public static bool ParseStringToCelestial(string str, out CelestialObject celestial)
        {
            if (string.IsNullOrEmpty(str))
            {
                celestial = new CelestialObject();
                return false;
            }

            char[] separators = { ' ', ',', '\t' };
            string[] parts = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 9)
            {
                celestial = new CelestialObject();
                return false;
            }

            string type = parts[0];

            string name = parts[1];
            string distStr = parts[2];
            string radiusStr = parts[3];
            string massStr = parts[4];
            string distFormatStr = parts[5];
            string radiusFormatStr = parts[6];
            string massesFormatStr = parts[7];

            List<string> mainElements = new List<string>();

            int len = 0;

            if (type == "star")
            {
                len = parts.Length - 2;
            }
            else if (type == "planet")
            {
                len = parts.Length - 1;
            }
            else if (type == "satellite")
            {
                len = parts.Length - 1;
            }
            else
            {
                len = parts.Length;
            }

            for (int i = 8; i < len; i++)
            {
                mainElements.Add(parts[i]);
            }

            double dist;
            bool result = double.TryParse(distStr, out dist);
            if (!result || dist < 0.0)
            {
                celestial = new CelestialObject();
                return false;
            }

            double radius;
            result = double.TryParse(radiusStr, out radius);
            if (!result || radius < 0.0)
            {
                celestial = new CelestialObject();
                return false;
            }

            double mass;
            result = double.TryParse(massStr, out mass);
            if (!result || mass < 0.0)
            {
                celestial = new CelestialObject();
                return false;
            }

            CelestialObject.MassFormat massFormat;
            switch (massesFormatStr)
            {
                case "kg":
                    massFormat = CelestialObject.MassFormat.Kilograms;
                    break;
                case "SM":
                    massFormat = CelestialObject.MassFormat.SolarMass;
                    break;
                default:
                    celestial = new CelestialObject();
                    return false;
            }

            CelestialObject.DistanceFormat distanceFormat;
            switch (distFormatStr)
            {
                case "km":
                    distanceFormat = CelestialObject.DistanceFormat.Kilometers;
                    break;
                case "pc":
                    distanceFormat = CelestialObject.DistanceFormat.Parsec;
                    break;
                case "ly":
                    distanceFormat = CelestialObject.DistanceFormat.LightYear;
                    break;
                case "au":
                    distanceFormat = CelestialObject.DistanceFormat.AstronomicalUnit;
                    break;
                default:
                    celestial = new CelestialObject();
                    return false;
            }

            CelestialObject.RadiusFormat radiusFormat;
            switch (radiusFormatStr)
            {
                case "km":
                    radiusFormat = CelestialObject.RadiusFormat.Kilometers;
                    break;
                case "SR":
                    radiusFormat = CelestialObject.RadiusFormat.SolarRadius;
                    break;
                default:
                    celestial = new CelestialObject();
                    return false;
            }

            if (type == "star")
            {
                // Считываем тип спектра
                char spectrum = parts[parts.Length - 2][0];
                if (spectrum != 'O' && spectrum != 'B' && spectrum != 'A' && spectrum != 'F' && spectrum != 'G' && spectrum != 'K' && spectrum != 'M')
                {
                    celestial = new Star();
                    return false;
                }

                // Считываем температуру
                string tempString = parts[parts.Length - 1];
                if (!double.TryParse(tempString, out double temperature))
                {
                    celestial = new Star();
                    return false;
                }

                celestial = new Star(mainElements, name, dist, radius, mass, massFormat, distanceFormat, radiusFormat, spectrum, temperature);
            }
            else if (type == "planet")
            {
                // Считываем звезду, вокруг которой она обращается
                string orbitingAround = parts[parts.Length - 1];

                celestial = new Planet(mainElements, orbitingAround, name, dist, radius, mass, massFormat, distanceFormat, radiusFormat);
            }
            else if (type == "satellite")
            {
                // Считываем планету, вокруг которой вращается спутник
                string orbitingAround = parts[parts.Length - 1];

                celestial = new Satellite(mainElements, name, dist, radius, mass, massFormat, distanceFormat, radiusFormat, orbitingAround);
            }
            else
            {
                celestial = new CelestialObject(mainElements, name, type, dist, radius, mass, massFormat, distanceFormat, radiusFormat);
            }

            return true;
        }

        public static void ReadCelestials(string fileName, List<CelestialObject> readObjects)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);

            foreach (string celestialLine in lines)
            {
                CelestialObject currentObject;
                if (!ParseStringToCelestial(celestialLine, out currentObject))
                {
                    return;
                }

                readObjects.Add(currentObject);
            }
        }

        public static IntPtr InitAllegro(int screenWidth, int screenHeight)
        {
            Allegro.InstallSystem(Allegro.VERSION_INT, IntPtr.Zero);
            IntPtr display = Allegro.CreateDisplay(screenWidth, screenHeight);
            Allegro.InitPrimitivesAddon();
            Allegro.InitFontAddon();
            Allegro.InitTTFAddon();
            Allegro.InstallKeyboard();
            Allegro.InstallMouse();
            return display;
        }

        public static void DestroyAllegro(IntPtr display)
        {
            Allegro.UninstallMouse();
            Allegro.UninstallKeyboard();
            Allegro.ShutdownPrimitivesAddon();
            Allegro.ShutfownTTFAddon();
            Allegro.DestroyDisplay(display);
            Allegro.UninstallSystem();
        }

        public static IntPtr LoadFont()
        {
            return Allegro.LoadTTFFont("FreeMono.ttf", 12, 0);
        }

        public static void PrintCelestialInfo(CelestialObject celestial, IntPtr font)
        {
            Allegro.DrawMultilineText(font, Allegro.MapRGB(255, 255, 255), 20, 20, 400, 15, 0, celestial.ToString());
        }

        public static void DrawCelestials(List<CelestialObject> celestials, Allegro.MouseState mouse, IntPtr font, double camAngleA, double camAngleB)
        {
            List<ScreenObject> projections = new List<ScreenObject>();

            foreach (CelestialObject celestial in celestials)
            {
                celestial.RotateAroundY(camAngleA);
                celestial.RotateAroundX(camAngleB);

                projections.Add(new ScreenObject(celestial));

                celestial.RotateAroundX(-camAngleB);
                celestial.RotateAroundY(-camAngleA);
            }

            GetClickedObject(mouse, celestials, projections);

            for (int i = 0; i < projections.Count; i++)
            {
                ScreenObject projection = projections[i];
                projection.Draw();

                if (celestials[i].Clicked)
                {
                    Allegro.DrawCircle(projection.X, projection.Y, projection.Radius + 8, Allegro.MapRGB(200, 200, 0), 2.0f);
                    PrintCelestialInfo(celestials[i], font);
                }
            }
        }

        public static void DrawGround(double horY, double horZ, bool solid = false)
        {
            if (horZ <= 0) { return; }

            double yScreenKm = horY * projectionCoef / horZ + screenHeightInKm / 2;
            int yScreen = ScreenObject.KmToPixels(yScreenKm);

            Allegro.DrawLine(0.0f, yScreen, screenWidth, yScreen, Allegro.MapRGB(0, 200, 0), 2.0f);

            if (solid)
            {
                Allegro.DrawFilledRectangle(0, screenHeight, screenWidth, yScreen, Allegro.MapRGB(0, 150, 0));
            }
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static void GetClickedObject(Allegro.MouseState mouse, List<CelestialObject> celestials, List<ScreenObject> projections)
        {
            int mx = mouse.x;
            int my = mouse.y;
            bool buttonPressed = (mouse.buttons & 1) != 0;

            if (!buttonPressed)
            {
                return;
            }

            for (int i = 0; i < projections.Count; i++)
            {
                ScreenObject proj = projections[i];
                double dist = GetDistance(mx, my, proj.X, proj.Y);

                if (dist <= proj.Radius)
                {
                    celestials[i].Clicked = true;
                }
                else
                {
                    celestials[i].Clicked = false;
                }
            }
        }

        public static void PrintCelestialCoords(List<CelestialObject> celestials)
        {
            for (int i = 0; i < celestials.Count; i++)
            {
                Console.WriteLine("Coords: " + celestials[i].XWorld + " " + celestials[i].YWorld + " " + celestials[i].ZWorld);
            }
            Console.WriteLine("---------------------------");
        }

        public static int Main(string[] args)
        {
            List<CelestialObject> celestials = new List<CelestialObject>();
            ReadCelestials("celestials.txt", celestials);

            IntPtr display = InitAllegro(screenWidth, screenHeight);
            IntPtr freeMonoFont = LoadFont();
            Allegro.KeyboardState keyboard;
            Allegro.MouseState mouse = new Allegro.MouseState();

            double horZ = 5000.0, horY = 0.0;
            double camAngleA = 0.0, camAngleB = 0.0;

            bool ground = true;
            bool spaceWasPressed = false;
            bool run = true;

            while (run)
            {
                unsafe
                {
                    Allegro.GetMouseState(&mouse);
                    Allegro.GetKeyboardState(&keyboard);

                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_SPACE))
                    {
                        if (!spaceWasPressed)
                        {
                            ground = !ground;
                            spaceWasPressed = true;
                        }
                    }
                    else
                    {
                        spaceWasPressed = false;
                    }
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_ESCAPE))
                    {
                        run = false;
                    }
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_A))
                    {
                        camAngleA -= 1.0 / 180.0 * 3.14;
                    }
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_D))
                    {
                        camAngleA += 1.0 / 180.0 * 3.14;
                    }
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_W))
                    {
                        Transformation.RotatePoint(ref horZ, ref horY, 1.0 / 180.0f * 3.14);
                        camAngleB += 1.0 / 180.0 * 3.14;
                    }
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_S))
                    {
                        Transformation.RotatePoint(ref horZ, ref horY, -1.0 / 180.0f * 3.14);
                        camAngleB -= 1.0 / 180.0 * 3.14;
                    }
                }

                Allegro.ClearToColor(Allegro.MapRGB(0, 0, 0));

                DrawCelestials(celestials, mouse, freeMonoFont, camAngleA, camAngleB);
                DrawGround(horY, horZ, ground);

                Allegro.FlipDisplay();
                Allegro.Rest(0.01);
            }

            Allegro.DestroyFont(freeMonoFont);
            DestroyAllegro(display);

            return 0;
        }
    }
}