using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel
{
    public class OutOfLevelCheckSystem : IEcsRunSystem
    {
        private readonly ILevelService level;

        public OutOfLevelCheckSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CPosition>().End();
            var positionPool = world.GetPool<CPosition>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                if (level.IsOut(position))
                {
                    world.NewEntityWith(new COutOfLevelEvent { PackedEntity = world.PackEntity(entity) });
                }
            }
        }
    }
}