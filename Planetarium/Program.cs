using System;
using System.Collections.Generic;
using Sky;

namespace Planetarium
{
    class MainClass
    {
        public struct Camera
        {
            public double angleA, angleB;
            public double horY, horZ;
            public bool ground;

            public Camera(double angleA = 0.0, double angleB = 0.0, double horY = 0.0, double horZ = 0.0, bool ground = true)
            {
                this.angleA = angleA;
                this.angleB = angleB;
                this.horY = horY;
                this.horZ = horZ;
                this.ground = ground;
            }
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static List<Star> GenerateStars(int n)
        {
            List<Star> stars = new List<Star>();

            for (int i = 0; i < n; i++)
            {
                stars.Add(Star.GenerateRandom());
            }

            return stars;
        }

        public static List<Planet> GeneratePlanets(int n, List<Star> orbitingAroundList)
        {
            Random random = new Random();
            List<Planet> planets = new List<Planet>();

            for (int i = 0; i < n; i++)
            {
                Planet current = Planet.GenerateRandom(orbitingAroundList[random.Next(orbitingAroundList.Count)]);
                planets.Add(current);
            }

            return planets;
        }

        public static List<Satellite> GenerateSatellites(int n, List<Planet> planets)
        {
            Random random = new Random();

            List<Satellite> satellites = new List<Satellite>();

            for (int i = 0; i < n; i++)
            {
                satellites.Add(Satellite.GenerateRandom(planets[random.Next(planets.Count)]));
            }

            return satellites;
        }

        public static List<CelestialObject> GenerateCelestials(int numObjects = 9)
        {
            // Cоздать звёзды (это независимые объекты). ЗАТЕМ создать планеты вокруг звёзд.
            // Затем создать спутники вокруг планет.

            List<CelestialObject> celestials = new List<CelestialObject>();
            List<Star> stars = GenerateStars(numObjects / 3);
            List<Planet> planets = GeneratePlanets(numObjects / 3, stars);
            List<Satellite> satellites = GenerateSatellites(numObjects / 3, planets);

            foreach (Star star in stars) { celestials.Add(star); }
            foreach (Planet planet in planets) { celestials.Add(planet); }
            foreach (Satellite satellite in satellites) { celestials.Add(satellite); }

            return celestials;
        }

        public static IntPtr InitAllegro(int screenWidth, int screenHeight)
        {
            Allegro.InstallSystem(Allegro.VERSION_INT, IntPtr.Zero);
            Allegro.SetNewDisplayFlags(Allegro.FULLSCREEN);
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
            Allegro.DestroyDisplay(display);
            Allegro.UninstallMouse();
            Allegro.UninstallKeyboard();
            Allegro.ShutdownPrimitivesAddon();
            Allegro.ShutfownTTFAddon();
            Allegro.UninstallSystem();
        }

        public static IntPtr LoadFont()
        {
            return Allegro.LoadTTFFont("FreeMono.ttf", 12, 0);
        }

        public static void DrawGround(double horY, double horZ, bool solid = false)
        {
            if (horZ <= 0) { return; }

            double yScreenKm = horY * ScreenParameters.projectionCoef / horZ + ScreenParameters.screenHeightInKm / 2;
            int yScreen = ScreenParameters.KmToPixels(yScreenKm);

            Allegro.DrawLine(0.0f, yScreen, ScreenParameters.screenWidth, yScreen, Allegro.MapRGB(0, 200, 0), 2.0f);

            if (solid)
            {
                Allegro.DrawFilledRectangle(0, ScreenParameters.screenHeight, ScreenParameters.screenWidth, yScreen, Allegro.MapRGB(0, 150, 0));
            }
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static void PrintCelestialCoords(List<CelestialObject> celestials)
        {
            for (int i = 0; i < celestials.Count; i++)
            {
                Console.WriteLine("Coords: " + celestials[i].XWorld + " " + celestials[i].YWorld + " " + celestials[i].ZWorld);
            }
            Console.WriteLine("---------------------------");
        }

        public delegate void KeyPressHandler(Allegro.KeyboardState keyboard, ref Camera camera, ref bool spaceWasPressed);

        public static event KeyPressHandler KeyPress;

        public static void SetKeyPressHandlers()
        {
            unsafe
            {
                KeyPress += delegate (Allegro.KeyboardState keyboard, ref Camera playerCam, ref bool spaceWasPressed)
                {
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_W))
                    {
                        Transformation.RotatePoint(ref playerCam.horZ, ref playerCam.horY, Transformation.DegToRad(1.0));
                        playerCam.angleB += Transformation.DegToRad(1.0);
                    }
                };
                KeyPress += delegate (Allegro.KeyboardState keyboard, ref Camera playerCam, ref bool spaceWasPressed)
                {
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_S))
                    {
                        Transformation.RotatePoint(ref playerCam.horZ, ref playerCam.horY, -Transformation.DegToRad(1.0));
                        playerCam.angleB -= Transformation.DegToRad(1.0);
                    }
                };
                KeyPress += (Allegro.KeyboardState keyboard, ref Camera playerCam, ref bool spaceWasPressed) =>
                {
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_A))
                    {
                        playerCam.angleA -= Transformation.DegToRad(1.0);
                    }
                };
                KeyPress += (Allegro.KeyboardState keyboard, ref Camera playerCam, ref bool spaceWasPressed) =>
                {
                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_D))
                    {
                        playerCam.angleA += Transformation.DegToRad(1.0);
                    }
                };
            }

            KeyPress += KeySpacePress;
        }

        public static void KeySpacePress(Allegro.KeyboardState keyboard, ref Camera playerCam, ref bool spaceWasPressed)
        {
            unsafe
            {
                if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_SPACE))
                {
                    if (!spaceWasPressed)
                    {
                        playerCam.ground = !playerCam.ground;
                        spaceWasPressed = true;
                    }
                }
                else
                {
                    spaceWasPressed = false;
                }
            }
        }

        public static void TestSystem()
        {
            OperatingSystem operatingSystem = Environment.OSVersion;

            string osStr = operatingSystem.VersionString;
            string osName = osStr.Split()[0];

            if (osName == "Unix")
            {
                Console.WriteLine("Linux");
            }
            else
            {
                Console.WriteLine("Windows");
            }
        }

        public static int Main(string[] args)
        {
            TestSystem();

            List<CelestialObject> celestials = GenerateCelestials(999);

            IntPtr display = InitAllegro(ScreenParameters.screenWidth, ScreenParameters.screenHeight);
            IntPtr freeMonoFont = LoadFont();
            CelestialObject.FontForDisplayingInfo = freeMonoFont;
            Allegro.KeyboardState keyboard;
            Allegro.MouseState mouse = new Allegro.MouseState();

            Camera playerCam = new Camera(0.0, 0.0, 0.0, 5000.0, true);

            SetKeyPressHandlers();

            bool spaceWasPressed = false;
            bool run = true;

            while (run)
            {
                unsafe
                {
                    Allegro.GetMouseState(&mouse);
                    Allegro.GetKeyboardState(&keyboard);

                    if (Allegro.KeyDown(&keyboard, Allegro.KeyCode.ALLEGRO_KEY_ESCAPE))
                    {
                        run = false;
                    }

                    KeyPress?.Invoke(keyboard, ref playerCam, ref spaceWasPressed);
                }

                celestials.Sort();

                foreach (CelestialObject celestial in celestials)
                {
                    celestial.Update(mouse, playerCam.angleA, playerCam.angleB);
                }

                Allegro.ClearToColor(Allegro.MapRGB(0, 0, 0));

                foreach (CelestialObject celestial in celestials)
                {
                    celestial.Draw();
                }

                DrawGround(playerCam.horY, playerCam.horZ, playerCam.ground);

                Allegro.FlipDisplay();
                Allegro.Rest(0.01);
            }

            Allegro.DestroyFont(freeMonoFont);

            DestroyAllegro(display);

            return 0;
        }
    }
}