using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OutOfLevelCheckSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ILevelService level;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CPosition> positionPool;

        public OutOfLevelCheckSystem(ILevelService level)
        {
            this.level = level;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CPosition>().End();
            positionPool = world.GetPool<CPosition>();
        }

        public void Run(IEcsSystems systems)
        {
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