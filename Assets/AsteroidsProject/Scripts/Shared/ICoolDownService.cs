namespace AsteroidsProject.Shared
{
    public interface ICoolDownService
    {
        public float Value { get; set; }
        public float TimeLeft { get; }
        public bool IsReady { get; }
        public void Reset();
    }
}