namespace Sky
{
    public interface IDrawable
    {
        void Draw();
    }

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

    public interface IRotatingObject : IObjectInSpace
    {
        void MoveAlong();
        void RotateAround();
    }
}