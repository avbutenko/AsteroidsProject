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
            var filter = world.Filter<TeleportationRequest>()
                              .Inc<Position>()
                              .End();

            var teleportationRequestPool = world.GetPool<TeleportationRequest>();
            var positionPool = world.GetPool<Position>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                position = level.GetOppositePosition(position);
                teleportationRequestPool.Del(entity);
            }
        }
    }
}