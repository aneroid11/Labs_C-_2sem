namespace Sky
{
    public interface IRotatingObject : IObjectInSpace
    {
        void MoveAlong();
        void RotateAround();
    }
}
