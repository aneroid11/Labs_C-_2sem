namespace Sky
{
    public interface IObjectInSpace
    {
        double XWorld { get; set; }
        double YWorld { get; set; }
        double ZWorld { get; set; }
        double Radius { get; set; }

        void RotateAroundX(double phi);
        void RotateAroundY(double phi);
        Allegro.Color GetColor();
        void CreateProjection(double camAngleA, double camAngleB);
    }
}
