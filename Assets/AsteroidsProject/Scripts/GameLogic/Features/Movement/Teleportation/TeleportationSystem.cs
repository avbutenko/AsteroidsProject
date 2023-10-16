using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Teleportation
{
    public class TeleportationSystem : IEcsRunSystem
    {
        private readonly ILevelService level;

        public TeleportationSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CTeleportationRequest>()
                              .Inc<CPosition>()
                              .End();

            var teleportationRequestPool = world.GetPool<CTeleportationRequest>();
            var positionPool = world.GetPool<CPosition>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                position = level.GetOppositePosition(position);
                teleportationRequestPool.Del(entity);
            }
        }
    }
}