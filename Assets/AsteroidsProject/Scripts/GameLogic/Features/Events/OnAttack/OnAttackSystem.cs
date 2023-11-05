using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnAttack
{
    public class OnAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<COnAttack>()
                              .Inc<CAttackAllowed>()
                              .End();

            var onAttackPool = world.GetPool<COnAttack>();
            var attackAllowedPool = world.GetPool<CAttackAllowed>();

            foreach (var entity in filter)
            {
                ref var components = ref onAttackPool.Get(entity).AddToSelfComponents;
                world.AddRawComponentsToEntity(entity, components);
                attackAllowedPool.Del(entity);
            }
        }
    }
}

