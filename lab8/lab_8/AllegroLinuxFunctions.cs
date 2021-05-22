using System;
using System.Runtime.InteropServices;
using static Allegro;

public static class AllegroLinuxFunctions
{
    [DllImport("liballegro.so", EntryPoint = "al_install_system")]
    public extern static bool InstallSystem(int version, IntPtr atExit);

    [DllImport("liballegro.so", EntryPoint = "al_uninstall_system")]
    public extern static void UninstallSystem();

    [DllImport("liballegro.so", EntryPoint = "al_create_display")]
    public extern static IntPtr CreateDisplay(int screenWidth, int screenHeight);

    [DllImport("liballegro.so", EntryPoint = "al_set_new_display_flags")]
    public extern static void SetNewDisplayFlags(int flags);

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

    [DllImport("liballegro_primitives.so", EntryPoint = "al_draw_filled_rectangle")]
    public extern static void DrawFilledRectangle(float x1, float y1, float x2, float y2, Color color);

    [DllImport("liballegro_primitives.so", EntryPoint = "al_draw_filled_circle")]
    public extern static void DrawFilledCircle(float cx, float cy, float r, Color color);

    [DllImport("liballegro_primitives.so", EntryPoint = "al_draw_circle")]
    public extern static void DrawCircle(float cx, float cy, float r, Color color, float thickness);

    [DllImport("liballegro_primitives.so", EntryPoint = "al_draw_line")]
    public extern static void DrawLine(float x1, float y1, float x2, float y2, Color color, float thickness);

    [DllImport("liballegro.so", EntryPoint = "al_rest")]
    public extern static void Rest(double seconds);

    [DllImport("liballegro.so", EntryPoint = "al_install_keyboard")]
    public extern static bool InstallKeyboard();

    [DllImport("liballegro.so", EntryPoint = "al_uninstall_keyboard")]
    public extern static void UninstallKeyboard();

    [DllImport("liballegro.so", EntryPoint = "al_get_keyboard_state")]
    public unsafe extern static void GetKeyboardState(KeyboardState* keyboard);

    [DllImport("liballegro.so", EntryPoint = "al_key_down")]
    public unsafe extern static bool KeyDown(KeyboardState* keyboard, KeyCode keycode);

    [DllImport("liballegro.so", EntryPoint = "al_install_mouse")]
    public extern static void InstallMouse();

    [DllImport("liballegro.so", EntryPoint = "al_uninstall_mouse")]
    public extern static void UninstallMouse();

    [DllImport("liballegro.so", EntryPoint = "al_get_mouse_state")]
    public unsafe extern static void GetMouseState(MouseState* retState);

    [DllImport("liballegro.so", EntryPoint = "al_mouse_button_down")]
    public unsafe extern static bool MouseButtonDown(MouseState* state, int button);

    [DllImport("liballegro_ttf.so", EntryPoint = "al_init_font_addon")]
    public extern static void InitFontAddon();

    [DllImport("liballegro_ttf.so", EntryPoint = "al_init_ttf_addon")]
    public extern static bool InitTTFAddon();

    [DllImport("liballegro_ttf.so", EntryPoint = "al_shutdown_ttf_addon")]
    public extern static bool ShutfownTTFAddon();

    [DllImport("liballegro_ttf.so", EntryPoint = "al_load_ttf_font")]
    public extern static IntPtr LoadTTFFont(string filename, int size, int flags);

    [DllImport("liballegro_ttf.so", EntryPoint = "al_destroy_font")]
    public extern static void DestroyFont(IntPtr ptr);

    [DllImport("liballegro_ttf.so", EntryPoint = "al_draw_multiline_text")]
    public extern static void DrawMultilineText(IntPtr font, Color cl, float x, float y, float max_width, float line_height, int flags, string text);
}
