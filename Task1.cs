using System;
using System.Runtime.InteropServices;

namespace Lab4_task1
{
    static class Allegro
    {
        public const int VERSION = 5;
        public const int SUB_VERSION = 2;
        public const int WIP_VERSION = 6;
        public const int RELEASE_NUMBER = 1;
        public const int UNSTABLE_BIT = 0;
        public const int VERSION_INT = (VERSION << 24) | (SUB_VERSION << 16) | (WIP_VERSION << 8) | RELEASE_NUMBER | UNSTABLE_BIT;

        public struct Color
        {
            float r, g, b, a;
        }

        [DllImport("liballegro.so", EntryPoint = "al_install_system")]
        public extern static bool InstallSystem(int version, IntPtr atExit);

        [DllImport("liballegro.so", EntryPoint = "al_uninstall_system")]
        public extern static void UninstallSystem();

        [DllImport("liballegro.so", EntryPoint = "al_create_display")]
        public extern static IntPtr CreateDisplay(int screenWidth, int screenHeight);

        [DllImport("liballegro.so", EntryPoint = "al_destroy_display")]
        public extern static void DestroyDisplay(IntPtr display);

        [DllImport("liballegro_primitives.so", EntryPoint = "al_init_primitives_addon")]
        public extern static bool InitPrimitivesAddon();

        [DllImport("liballegro_primitives.so", EntryPoint = "al_shutdown_primitives_addon")]
        public extern static void ShutdownPrimitivesAddon();

        [DllImport("liballegro.so", EntryPoint = "al_map_rgb")]
        public extern static Color MapRGB(byte r, byte g, byte b);

        [DllImport("liballegro.so", EntryPoint = "al_clear_to_color")]
        public extern static void ClearToColor(Color color);

        [DllImport("liballegro.so", EntryPoint = "al_flip_display")]
        public extern static void FlipDisplay();

        [DllImport("liballegro_primitives.so", EntryPoint = "al_draw_filled_circle")]
        public extern static void DrawFilledCircle(float cx, float cy, float r, Color color);

        [DllImport("liballegro.so", EntryPoint = "al_rest")]
        public extern static void Rest(double seconds);
    }

    class Ball
    {
        private int _x, _y, _radius;
        private float _gravity;
        private float _velX;
        private float _velY;
        private Allegro.Color _color;

        public Ball(int x, int y, int radius, float gravity, float velX, Allegro.Color color)
        {
            _velX = velX;
            _velY = 0.0f;
            _gravity = gravity;
            _x = x;
            _y = y;
            _radius = radius;
            _color = color;
        }

        public void Update()
        {
            _velY += _gravity;
            _x += (int)_velX;
            _y += (int)_velY;

            if (_y > 480 - _radius)
            {
                _velY = -_velY;
                _y += (int)_velY;
            }

            if (_x < _radius || _x > 640 - _radius)
            {
                _velX = -_velX;
                _x += (int)_velX;
            }
        }

        public void Draw()
        {
            Allegro.DrawFilledCircle(_x, _y, _radius, _color);
        }
    }

    class MainClass
    {
        public static int Main(string[] args)
        {
            Allegro.InstallSystem(Allegro.VERSION_INT, IntPtr.Zero);
            IntPtr display = Allegro.CreateDisplay(640, 480);
            Allegro.InitPrimitivesAddon();

            Ball ball = new Ball(200, 200, 40, 0.5f, 3, Allegro.MapRGB(0, 200, 200));

            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (watch.ElapsedMilliseconds < 20000)
            {
                Allegro.ClearToColor(Allegro.MapRGB(0, 0, 0));

                ball.Update();
                ball.Draw();

                Allegro.FlipDisplay();
                Allegro.Rest(0.01);
            }

            watch.Stop();
            Allegro.ShutdownPrimitivesAddon();
            Allegro.DestroyDisplay(display);
            Allegro.UninstallSystem();
            return 0;
        }
    }
}
