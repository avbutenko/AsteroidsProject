using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class TimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}