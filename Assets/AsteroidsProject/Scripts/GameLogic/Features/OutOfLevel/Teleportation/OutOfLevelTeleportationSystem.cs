using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.OutOfLevel.Teleportation
{
    public class OutOfLevelTeleportationSystem : IEcsRunSystem
    {
        private readonly ILevelService level;

        public OutOfLevelTeleportationSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<COutOfLevelEvent>()
                              .Inc<COnOutOfLevelTeleportTag>()
                              .Inc<CPosition>()
                              .End();

            var eventPool = world.GetPool<COutOfLevelEvent>();
            var positionPool = world.GetPool<CPosition>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                position = level.GetOppositePosition(position);
                eventPool.Del(entity);
            }
        }
    }
}