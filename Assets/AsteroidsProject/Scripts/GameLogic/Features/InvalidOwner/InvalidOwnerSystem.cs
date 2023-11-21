using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.InvalidOwner
{
    public class InvalidOwnerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<COwnerEntity>().End();
            var ownerPool = world.GetPool<COwnerEntity>();

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