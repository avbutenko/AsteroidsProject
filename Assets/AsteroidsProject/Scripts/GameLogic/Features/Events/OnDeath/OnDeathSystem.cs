using Assets.AsteroidsProject.Scripts.Shared;
using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnDeath
{
    public class OnDeathSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CDeathEvent>().End();
            var eventPool = world.GetPool<CDeathEvent>();
            var goIDPool = world.GetPool<CGameObjectInstanceID>();

            foreach (var entity in filter)
            {
                ref var packedEntity = ref eventPool.Get(entity).PackedEntity;

                if (packedEntity.Unpack(world, out int deadEntity))
                {
                    HandleOnDeathEvents(world, deadEntity);
                    ref var deadGoID = ref goIDPool.Get(deadEntity).Value;
                    world.NewEntityWith(new CInvalidGameObjectInstanceID { Value = deadGoID });
                    world.DelEntity(deadEntity);
                }

                world.DelEntity(entity);
            }
        }

        private void HandleOnDeathEvents(EcsWorld world, int entity)
        {
            var onDeathPool = world.GetPool<COnDeath>();
            var positionPool = world.GetPool<CPosition>();

            if (onDeathPool.Has(entity))
            {
                ref var createList = ref onDeathPool.Get(entity).CreateEntities;

                foreach (var item in createList)
                {
                    var newEntity = world.NewEntity();

                    foreach (var component in item.Components)
                    {
                        if (component is IHavePosition)
                        {
                            ref var originPosition = ref positionPool.Get(entity).Value;
                            (component as IHavePosition).Position = originPosition;
                        }

                        world.AddRawComponentToEntity(newEntity, component);
                    }
                }
            }
        }
    }
}