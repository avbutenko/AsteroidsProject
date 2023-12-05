namespace AsteroidsProject.Shared
{
    public interface ITimeService
    {
        public float CurrentTime { get; }
        public float DeltaTime { get; }
        public bool IsPaused { get; }
        public void TooglePause();
    }
}