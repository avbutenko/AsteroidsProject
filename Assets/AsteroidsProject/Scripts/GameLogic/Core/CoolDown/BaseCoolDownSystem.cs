using Assets.AsteroidsProject.Scripts.Shared;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Core
{
    public abstract class BaseCoolDownSystem<T> : IEcsInitSystem, IEcsRunSystem where T : struct, IHaveTimer
    {
        private readonly ITimeService timeService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<T> coolDownPool;

        public BaseCoolDownSystem(ITimeService timeService)
        {
            this.timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<T>().End();
            coolDownPool = world.GetPool<T>();
        }

        public void Run(IEcsSystems systems)
        {
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