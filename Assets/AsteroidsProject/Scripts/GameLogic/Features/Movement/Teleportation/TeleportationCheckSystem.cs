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
            var filter = world.Filter<CTeleportableTag>()
                              .Inc<CPosition>()
                              .End();

            var positionPool = world.GetPool<CPosition>();
            var teleportationRequestPool = world.GetPool<CTeleportationRequest>();

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