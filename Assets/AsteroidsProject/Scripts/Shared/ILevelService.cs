namespace AsteroidsProject.Shared
{
    public interface ILevelService
    {
        public float Bottom { get; }
        public float Top { get; }
        public float Left { get; }
        public float Right { get; }
        public float ExtentHeight { get; }
        public float Height { get; }
        public float ExtentWidth { get; }
        public float Width { get; }
    }
}