using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnAttack
{
    public class OnAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CAttackEvent>().End();
            var eventPool = world.GetPool<CAttackEvent>();
            var onAttackPool = world.GetPool<COnAttack>();

            foreach (var entity in filter)
            {
                ref var attackingPackedEntity = ref eventPool.Get(entity).PackedEntity;

                if (attackingPackedEntity.Unpack(world, out var attackingEntity))
                {
                    if (onAttackPool.Has(attackingEntity))
                    {
                        ref var components = ref onAttackPool.Get(attackingEntity).AddToSelfComponents;
                        world.AddRawComponentsToEntity(attackingEntity, components);
                    }
                }

                eventPool.Del(entity);
            }
        }
    }
}