using AsteroidsProject.Infrastructure.Services;
using UnityEngine;

namespace AsteroidsProject.EngineRelated.Services
{
    public class TimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}