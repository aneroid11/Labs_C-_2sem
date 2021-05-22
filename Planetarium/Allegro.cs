using System;
using System.Reflection;

public static class Allegro
{
    public const int VERSION = 5;
    public const int SUB_VERSION = 2;
    public const int WIP_VERSION = 6;
    public const int RELEASE_NUMBER = 1;
    public const int UNSTABLE_BIT = 0;
    public const int VERSION_INT = (VERSION << 24) | (SUB_VERSION << 16) | (WIP_VERSION << 8) | RELEASE_NUMBER | UNSTABLE_BIT;

    public const int FULLSCREEN = 1 << 1;

    public struct Color
    {
        public float r, g, b, a;
    }

    public enum KeyCode
    {
        ALLEGRO_KEY_A = 1,
        ALLEGRO_KEY_B = 2,
        ALLEGRO_KEY_C = 3,
        ALLEGRO_KEY_D = 4,
        ALLEGRO_KEY_E = 5,
        ALLEGRO_KEY_F = 6,
        ALLEGRO_KEY_G = 7,
        ALLEGRO_KEY_H = 8,
        ALLEGRO_KEY_I = 9,
        ALLEGRO_KEY_J = 10,
        ALLEGRO_KEY_K = 11,
        ALLEGRO_KEY_L = 12,
        ALLEGRO_KEY_M = 13,
        ALLEGRO_KEY_N = 14,
        ALLEGRO_KEY_O = 15,
        ALLEGRO_KEY_P = 16,
        ALLEGRO_KEY_Q = 17,
        ALLEGRO_KEY_R = 18,
        ALLEGRO_KEY_S = 19,
        ALLEGRO_KEY_T = 20,
        ALLEGRO_KEY_U = 21,
        ALLEGRO_KEY_V = 22,
        ALLEGRO_KEY_W = 23,
        ALLEGRO_KEY_X = 24,
        ALLEGRO_KEY_Y = 25,
        ALLEGRO_KEY_Z = 26,

        ALLEGRO_KEY_0 = 27,
        ALLEGRO_KEY_1 = 28,
        ALLEGRO_KEY_2 = 29,
        ALLEGRO_KEY_3 = 30,
        ALLEGRO_KEY_4 = 31,
        ALLEGRO_KEY_5 = 32,
        ALLEGRO_KEY_6 = 33,
        ALLEGRO_KEY_7 = 34,
        ALLEGRO_KEY_8 = 35,
        ALLEGRO_KEY_9 = 36,

        ALLEGRO_KEY_PAD_0 = 37,
        ALLEGRO_KEY_PAD_1 = 38,
        ALLEGRO_KEY_PAD_2 = 39,
        ALLEGRO_KEY_PAD_3 = 40,
        ALLEGRO_KEY_PAD_4 = 41,
        ALLEGRO_KEY_PAD_5 = 42,
        ALLEGRO_KEY_PAD_6 = 43,
        ALLEGRO_KEY_PAD_7 = 44,
        ALLEGRO_KEY_PAD_8 = 45,
        ALLEGRO_KEY_PAD_9 = 46,

        ALLEGRO_KEY_F1 = 47,
        ALLEGRO_KEY_F2 = 48,
        ALLEGRO_KEY_F3 = 49,
        ALLEGRO_KEY_F4 = 50,
        ALLEGRO_KEY_F5 = 51,
        ALLEGRO_KEY_F6 = 52,
        ALLEGRO_KEY_F7 = 53,
        ALLEGRO_KEY_F8 = 54,
        ALLEGRO_KEY_F9 = 55,
        ALLEGRO_KEY_F10 = 56,
        ALLEGRO_KEY_F11 = 57,
        ALLEGRO_KEY_F12 = 58,

        ALLEGRO_KEY_ESCAPE = 59,
        ALLEGRO_KEY_TILDE = 60,
        ALLEGRO_KEY_MINUS = 61,
        ALLEGRO_KEY_EQUALS = 62,
        ALLEGRO_KEY_BACKSPACE = 63,
        ALLEGRO_KEY_TAB = 64,
        ALLEGRO_KEY_OPENBRACE = 65,
        ALLEGRO_KEY_CLOSEBRACE = 66,
        ALLEGRO_KEY_ENTER = 67,
        ALLEGRO_KEY_SEMICOLON = 68,
        ALLEGRO_KEY_QUOTE = 69,
        ALLEGRO_KEY_BACKSLASH = 70,
        ALLEGRO_KEY_BACKSLASH2 = 71,
        ALLEGRO_KEY_COMMA = 72,
        ALLEGRO_KEY_FULLSTOP = 73,
        ALLEGRO_KEY_SLASH = 74,
        ALLEGRO_KEY_SPACE = 75,

        ALLEGRO_KEY_INSERT = 76,
        ALLEGRO_KEY_DELETE = 77,
        ALLEGRO_KEY_HOME = 78,
        ALLEGRO_KEY_END = 79,
        ALLEGRO_KEY_PGUP = 80,
        ALLEGRO_KEY_PGDN = 81,
        ALLEGRO_KEY_LEFT = 82,
        ALLEGRO_KEY_RIGHT = 83,
        ALLEGRO_KEY_UP = 84,
        ALLEGRO_KEY_DOWN = 85,

        ALLEGRO_KEY_PAD_SLASH = 86,
        ALLEGRO_KEY_PAD_ASTERISK = 87,
        ALLEGRO_KEY_PAD_MINUS = 88,
        ALLEGRO_KEY_PAD_PLUS = 89,
        ALLEGRO_KEY_PAD_DELETE = 90,
        ALLEGRO_KEY_PAD_ENTER = 91,

        ALLEGRO_KEY_PRINTSCREEN = 92,
        ALLEGRO_KEY_PAUSE = 93,

        ALLEGRO_KEY_ABNT_C1 = 94,
        ALLEGRO_KEY_YEN = 95,
        ALLEGRO_KEY_KANA = 96,
        ALLEGRO_KEY_CONVERT = 97,
        ALLEGRO_KEY_NOCONVERT = 98,
        ALLEGRO_KEY_AT = 99,
        ALLEGRO_KEY_CIRCUMFLEX = 100,
        ALLEGRO_KEY_COLON2 = 101,
        ALLEGRO_KEY_KANJI = 102,

        ALLEGRO_KEY_PAD_EQUALS = 103, 
        ALLEGRO_KEY_BACKQUOTE = 104,  
        ALLEGRO_KEY_SEMICOLON2 = 105, 
        ALLEGRO_KEY_COMMAND = 106,

        ALLEGRO_KEY_BACK = 107,      
        ALLEGRO_KEY_VOLUME_UP = 108,
        ALLEGRO_KEY_VOLUME_DOWN = 109,

        ALLEGRO_KEY_SEARCH = 110,
        ALLEGRO_KEY_DPAD_CENTER = 111,
        ALLEGRO_KEY_BUTTON_X = 112,
        ALLEGRO_KEY_BUTTON_Y = 113,
        ALLEGRO_KEY_DPAD_UP = 114,
        ALLEGRO_KEY_DPAD_DOWN = 115,
        ALLEGRO_KEY_DPAD_LEFT = 116,
        ALLEGRO_KEY_DPAD_RIGHT = 117,
        ALLEGRO_KEY_SELECT = 118,
        ALLEGRO_KEY_START = 119,
        ALLEGRO_KEY_BUTTON_L1 = 120,
        ALLEGRO_KEY_BUTTON_R1 = 121,
        ALLEGRO_KEY_BUTTON_L2 = 122,
        ALLEGRO_KEY_BUTTON_R2 = 123,
        ALLEGRO_KEY_BUTTON_A = 124,
        ALLEGRO_KEY_BUTTON_B = 125,
        ALLEGRO_KEY_THUMBL = 126,
        ALLEGRO_KEY_THUMBR = 127,

        ALLEGRO_KEY_UNKNOWN = 128,

        ALLEGRO_KEY_MODIFIERS = 215,

        ALLEGRO_KEY_LSHIFT = 215,
        ALLEGRO_KEY_RSHIFT = 216,
        ALLEGRO_KEY_LCTRL = 217,
        ALLEGRO_KEY_RCTRL = 218,
        ALLEGRO_KEY_ALT = 219,
        ALLEGRO_KEY_ALTGR = 220,
        ALLEGRO_KEY_LWIN = 221,
        ALLEGRO_KEY_RWIN = 222,
        ALLEGRO_KEY_MENU = 223,
        ALLEGRO_KEY_SCROLLLOCK = 224,
        ALLEGRO_KEY_NUMLOCK = 225,
        ALLEGRO_KEY_CAPSLOCK = 226,

        ALLEGRO_KEY_MAX
    }

    public unsafe struct KeyboardState
    {
        public IntPtr display;
        public fixed uint keyDown[(int)KeyCode.ALLEGRO_KEY_MAX];
    }

    public unsafe struct MouseState
    {
        public int x;
        public int y;
        public int z;
        public int w;
        public fixed int more_axes[4];
        public int buttons;
        public float pressure;
        public IntPtr display;
    }

    public static bool Linux()
    {
        OperatingSystem operatingSystem = Environment.OSVersion;

        string osStr = operatingSystem.VersionString;
        string osName = osStr.Split()[0];
        return osName == "Unix";
    }

    static Allegro()
    {
        bool linux = Linux();

        unsafe
        {
            if (linux)
            {
                InstallSystem = AllegroLinuxFunctions.InstallSystem;
                UninstallSystem = AllegroLinuxFunctions.UninstallSystem;
                CreateDisplay = AllegroLinuxFunctions.CreateDisplay;
                SetNewDisplayFlags = AllegroLinuxFunctions.SetNewDisplayFlags;
                DestroyDisplay = AllegroLinuxFunctions.DestroyDisplay;
                InitPrimitivesAddon = AllegroLinuxFunctions.InitPrimitivesAddon;
                ShutdownPrimitivesAddon = AllegroLinuxFunctions.ShutdownPrimitivesAddon;
                MapRGB = AllegroLinuxFunctions.MapRGB;
                ClearToColor = AllegroLinuxFunctions.ClearToColor;
                FlipDisplay = AllegroLinuxFunctions.FlipDisplay;
                DrawFilledRectangle = AllegroLinuxFunctions.DrawFilledRectangle;
                DrawFilledCircle = AllegroLinuxFunctions.DrawFilledCircle;
                DrawLine = AllegroLinuxFunctions.DrawLine;
                DrawCircle = AllegroLinuxFunctions.DrawCircle;
                DrawMultilineText = AllegroLinuxFunctions.DrawMultilineText;
                Rest = AllegroLinuxFunctions.Rest;
                InstallMouse = AllegroLinuxFunctions.InstallMouse;
                InstallKeyboard = AllegroLinuxFunctions.InstallKeyboard;
                UninstallMouse = AllegroLinuxFunctions.UninstallSystem;
                UninstallKeyboard = AllegroLinuxFunctions.UninstallKeyboard;
                GetMouseState = AllegroLinuxFunctions.GetMouseState;
                MouseButtonDown = AllegroLinuxFunctions.MouseButtonDown;
                InitFontAddon = AllegroLinuxFunctions.InitFontAddon;
                InitTTFAddon = AllegroLinuxFunctions.InitTTFAddon;
                ShutfownTTFAddon = AllegroLinuxFunctions.ShutfownTTFAddon;
                LoadTTFFont = AllegroLinuxFunctions.LoadTTFFont;
                DestroyFont = AllegroLinuxFunctions.DestroyFont;
                GetKeyboardState = AllegroLinuxFunctions.GetKeyboardState;
                KeyDown = AllegroLinuxFunctions.KeyDown;
            }
            else
            {
                InstallSystem = AllegroWindowsFunctions.InstallSystem;
                UninstallSystem = AllegroWindowsFunctions.UninstallSystem;
                CreateDisplay = AllegroWindowsFunctions.CreateDisplay;
                SetNewDisplayFlags = AllegroWindowsFunctions.SetNewDisplayFlags;
                DestroyDisplay = AllegroWindowsFunctions.DestroyDisplay;
                InitPrimitivesAddon = AllegroWindowsFunctions.InitPrimitivesAddon;
                ShutdownPrimitivesAddon = AllegroWindowsFunctions.ShutdownPrimitivesAddon;
                MapRGB = AllegroWindowsFunctions.MapRGB;
                ClearToColor = AllegroWindowsFunctions.ClearToColor;
                FlipDisplay = AllegroWindowsFunctions.FlipDisplay;
                DrawFilledRectangle = AllegroWindowsFunctions.DrawFilledRectangle;
                DrawFilledCircle = AllegroWindowsFunctions.DrawFilledCircle;
                DrawLine = AllegroWindowsFunctions.DrawLine;
                DrawCircle = AllegroWindowsFunctions.DrawCircle;
                DrawMultilineText = AllegroWindowsFunctions.DrawMultilineText;
                Rest = AllegroWindowsFunctions.Rest;
                InstallMouse = AllegroWindowsFunctions.InstallMouse;
                InstallKeyboard = AllegroWindowsFunctions.InstallKeyboard;
                UninstallMouse = AllegroWindowsFunctions.UninstallSystem;
                UninstallKeyboard = AllegroWindowsFunctions.UninstallKeyboard;
                GetMouseState = AllegroWindowsFunctions.GetMouseState;
                MouseButtonDown = AllegroWindowsFunctions.MouseButtonDown;
                InitFontAddon = AllegroWindowsFunctions.InitFontAddon;
                InitTTFAddon = AllegroWindowsFunctions.InitTTFAddon;
                ShutfownTTFAddon = AllegroWindowsFunctions.ShutfownTTFAddon;
                LoadTTFFont = AllegroWindowsFunctions.LoadTTFFont;
                DestroyFont = AllegroWindowsFunctions.DestroyFont;
                GetKeyboardState = AllegroWindowsFunctions.GetKeyboardState;
                KeyDown = AllegroWindowsFunctions.KeyDown;
            }
        }
    }

    public delegate bool InstallSystemMethod(int version, IntPtr atExit);
    public delegate void UninstallSystemMethod();
    public delegate IntPtr CreateDisplayMethod(int screenWidth, int screenHeight);
    public delegate void SetNewDisplayFlagsMethod(int flags);
    public delegate void DestroyDisplayMethod(IntPtr display);
    public delegate bool InitPrimitivesAddonMethod();
    public delegate void ShutdownPrimitivesAddonMethod();
    public delegate Color MapRGBMethod(byte r, byte g, byte b);
    public delegate void ClearToColorMethod(Color color);
    public delegate void FlipDisplayMethod();
    public delegate void DrawFilledRectangleMethod(float x1, float y1, float x2, float y2, Color color);
    public delegate void DrawFilledCircleMethod(float cx, float cy, float r, Color color);
    public delegate void DrawCircleMethod(float cx, float cy, float r, Color color, float thickness);
    public delegate void DrawLineMethod(float x1, float y1, float x2, float y2, Color color, float thickness);
    public delegate void RestMethod(double seconds);
    public delegate bool InstallKeyboardMethod();
    public delegate void UninstallKeyboardMethod();
    public unsafe delegate void GetKeyboardStateMethod(KeyboardState* keyboard);
    public unsafe delegate bool KeyDownMethod(KeyboardState* keyboard, KeyCode keycode);
    public delegate void InstallMouseMethod();
    public delegate void UninstallMouseMethod();
    public unsafe delegate void GetMouseStateMethod(MouseState* retState);
    public unsafe delegate bool MouseButtonDownMethod(MouseState* state, int button);
    public delegate void InitFontAddonMethod();
    public delegate bool InitTTFAddonMethod();
    public delegate bool ShutfownTTFAddonMethod();
    public delegate IntPtr LoadTTFFontMethod(string filename, int size, int flags);
    public delegate void DestroyFontMethod(IntPtr ptr);
    public delegate void DrawMultilineTextMethod(IntPtr font, Color cl, float x, float y, float max_width, float line_height, int flags, string text);

    public static InstallSystemMethod InstallSystem;
    public static UninstallSystemMethod UninstallSystem;
    public static CreateDisplayMethod CreateDisplay;
    public static SetNewDisplayFlagsMethod SetNewDisplayFlags;
    public static DestroyDisplayMethod DestroyDisplay;
    public static InitPrimitivesAddonMethod InitPrimitivesAddon;
    public static ShutdownPrimitivesAddonMethod ShutdownPrimitivesAddon;
    public static MapRGBMethod MapRGB;
    public static ClearToColorMethod ClearToColor;
    public static FlipDisplayMethod FlipDisplay;
    public static DrawFilledRectangleMethod DrawFilledRectangle;
    public static DrawFilledCircleMethod DrawFilledCircle;
    public static DrawLineMethod DrawLine;
    public static DrawCircleMethod DrawCircle;
    public static DrawMultilineTextMethod DrawMultilineText;
    public static RestMethod Rest;
    public static InstallMouseMethod InstallMouse;
    public static InstallKeyboardMethod InstallKeyboard;
    public static UninstallMouseMethod UninstallMouse;
    public static UninstallKeyboardMethod UninstallKeyboard;
    public static GetMouseStateMethod GetMouseState;
    public static MouseButtonDownMethod MouseButtonDown;
    public static InitFontAddonMethod InitFontAddon;
    public static InitTTFAddonMethod InitTTFAddon;
    public static ShutfownTTFAddonMethod ShutfownTTFAddon;
    public static LoadTTFFontMethod LoadTTFFont;
    public static DestroyFontMethod DestroyFont;
    public static GetKeyboardStateMethod GetKeyboardState;
    public static KeyDownMethod KeyDown;
}