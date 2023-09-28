using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class CoolDownService : ICoolDownService
    {
        private readonly ITimeService timeService;
        private float value;
        private float timesUp;

        public CoolDownService(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public float Value
        {
            get => this.value;
            set => this.value = value;
        }

        public float TimeLeft => Mathf.Max(timesUp - timeService.Time, 0);

        public bool IsReady => timesUp <= timeService.Time;

        public void Reset()
        {
            timesUp = timeService.Time + value;
        }
    }
}