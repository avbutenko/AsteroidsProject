using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class TimeService : ITimeService
    {
        private float timeScale;

        public TimeService()
        {
            timeScale = Time.timeScale;
        }

        public float CurrentTime => Time.time;
        public float DeltaTime => Time.deltaTime;
        public bool IsPaused => Time.timeScale == 0f;

        public void TooglePause()
        {
            if (!IsPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = timeScale;
            }
        }
    }
}