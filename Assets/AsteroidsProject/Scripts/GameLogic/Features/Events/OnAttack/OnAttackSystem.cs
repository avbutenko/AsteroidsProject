using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAttackEvent> eventPool;
        private EcsPool<COnAttack> onAttackPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CAttackEvent>().End();
            eventPool = world.GetPool<CAttackEvent>();
            onAttackPool = world.GetPool<COnAttack>();
        }

        public void Run(IEcsSystems systems)
        {
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