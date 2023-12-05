using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class TimeService : ITimeService
    {
        private float fixedDeltaTime;
        private float timeScale;

        public TimeService()
        {
            fixedDeltaTime = Time.fixedDeltaTime;
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

            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }
    }
}