namespace Sky
{
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

            double xScreen = celestial.XWorld * ScreenParameters.projectionCoef / celestial.ZWorld + ScreenParameters.screenWidthInKm / 2;
            double yScreen = celestial.YWorld * ScreenParameters.projectionCoef / celestial.ZWorld + ScreenParameters.screenHeightInKm / 2;

            double celRadInKm = 0.0;
            CelestialObject.RadiusFormat oldFormat = celestial.RadiusesFormat;
            celestial.Convert(CelestialObject.RadiusFormat.Kilometers);
            celRadInKm = celestial.Radius;
            celestial.Convert(oldFormat);

            double radiusProjection = celRadInKm * ScreenParameters.projectionCoef / celestial.ZWorld;

            X = ScreenParameters.KmToPixels(xScreen);
            Y = ScreenParameters.KmToPixels(yScreen);
            Radius = ScreenParameters.KmToPixels(radiusProjection);
            Visible = true;

            if (Radius < 3)
            {
                // Если объект слишком далеко, то проекция вырождается практически в точку
                Radius = 3;
            }

            Color = celestial.GetColor();
        }

        public void Draw()
        {
            if (!Visible) { return; }

            Allegro.DrawFilledCircle(X, Y, Radius, Color);
        }
    }
}