using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class InvalidOwnerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<COwnerEntity> ownerPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<COwnerEntity>().End();
            ownerPool = world.GetPool<COwnerEntity>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var owner = ref ownerPool.Get(entity).Value;

                if (!owner.Unpack(world, out _))
                {
                    world.NewEntityWith(new CDeathEvent { PackedEntity = world.PackEntity(entity) });
                    ownerPool.Del(entity);
                }
            }
        }
    }
}