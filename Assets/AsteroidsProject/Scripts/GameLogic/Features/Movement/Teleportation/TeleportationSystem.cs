using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Movement
{
    public class TeleportationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ILevelService level;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CTeleportationRequest> requestPool;
        private EcsPool<CPosition> positionPool;

        public TeleportationSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CTeleportationRequest>()
                          .Inc<CPosition>()
                          .End();

            requestPool = world.GetPool<CTeleportationRequest>();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                position = level.GetOppositePosition(position);
                requestPool.Del(entity);
            }
        }
    }
}