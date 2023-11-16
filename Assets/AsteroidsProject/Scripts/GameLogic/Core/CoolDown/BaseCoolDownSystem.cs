using Assets.AsteroidsProject.Scripts.Shared;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Core
{
    public abstract class BaseCoolDownSystem<T> : IEcsRunSystem where T : struct, IHaveTimer
    {
        private readonly ITimeService timeService;

        public BaseCoolDownSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<T>().End();
            var coolDownPool = world.GetPool<T>();

            foreach (var entity in filter)
            {
                ref var coolDown = ref coolDownPool.Get(entity);
                coolDown.Timer -= timeService.DeltaTime;

                if (coolDown.Timer <= 0)
                {
                    coolDownPool.Del(entity);
                }
            }
        }
    }
}