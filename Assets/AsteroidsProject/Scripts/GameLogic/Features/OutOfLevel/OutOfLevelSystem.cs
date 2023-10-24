using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.OutOfLevel.Check
{
    public class OutOfLevelSystem : IEcsRunSystem
    {
        private readonly ILevelService level;

        public OutOfLevelSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CPosition>()
                              .Inc<COnOutOfLevel>()
                              .End();

            var positionPool = world.GetPool<CPosition>();
            var onOutOfLevelPool = world.GetPool<COnOutOfLevel>();

            foreach (var entity in filter)
            {
                ref var position = ref positionPool.Get(entity).Value;

                if (level.IsOut(position))
                {
                    ref var components = ref onOutOfLevelPool.Get(entity).Components;
                    foreach (var component in components)
                    {
                        world.AddRawComponentToEntity(entity, component);
                    }
                }
            }
        }
    }
}