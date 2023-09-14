namespace AsteroidsProject.Shared
{
    public interface IInputService
    {
        public bool IsAccelerating { get; }
        public bool IsDeaccelerating { get; }
        public bool IsRotating { get; }
        public float RotationDirection { get; }
    }
}