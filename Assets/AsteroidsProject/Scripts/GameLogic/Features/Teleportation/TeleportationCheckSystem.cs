using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Teleportation
{
    public class TeleportationCheckSystem : IEcsRunSystem
    {
        private readonly ILevelService level;

        public TeleportationCheckSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TeleportableTag>()
                              .Inc<Position>()
                              .End();

            var positionPool = world.GetPool<Position>();
            var teleportationRequestPool = world.GetPool<TeleportationRequest>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                if (level.IsOut(position))
                {
                    teleportationRequestPool.Add(entity);
                }
            }
        }
    }
}