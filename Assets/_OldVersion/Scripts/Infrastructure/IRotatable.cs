namespace AsteroidsProject.Infrastructure
{
    public interface IRotatable
    {
        public float RatationSpeed { get; set; }
        public int RotationDirection { get; }
    }
}