using AsteroidsProject.Shared;

namespace AsteroidsProject.Services
{
    public class TimeService : ITimeService
    {
        public float Time => UnityEngine.Time.time;
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}