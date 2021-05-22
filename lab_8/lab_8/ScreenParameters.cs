namespace Sky
{
    public static class ScreenParameters
    {
        public const int screenWidth = 1920;
        public const int screenHeight = 1080;
        public const double screenWidthInKm = 0.0004;
        public const double screenHeightInKm = 0.0003;
        public const double kmInPixel = screenWidthInKm / screenWidth;
        public const double projectionCoef = 0.001;

        public static double PixelsToKm(int pixels)
        {
            return pixels * kmInPixel;
        }

        public static int KmToPixels(double km)
        {
            return (int)(km / kmInPixel);
        }
    }
}
