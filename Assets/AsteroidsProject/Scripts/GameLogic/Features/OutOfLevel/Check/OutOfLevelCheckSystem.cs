using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.OutOfLevel.Check
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
            var eventPool = world.GetPool<COutOfLevelEvent>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                if (level.IsOut(position))
                {
                    eventPool.Add(entity);
                }
            }
        }
    }
}