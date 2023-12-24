using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnDeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CDeathEvent> eventPool;
        private EcsPool<CGameObjectInstanceID> goIDPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CDeathEvent>().End();
            eventPool = world.GetPool<CDeathEvent>();
            goIDPool = world.GetPool<CGameObjectInstanceID>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var packedEntity = ref eventPool.Get(entity).PackedEntity;

                if (packedEntity.Unpack(world, out int deadEntity))
                {
                    HandleOnDeathEvents(deadEntity);
                    ref var deadGoID = ref goIDPool.Get(deadEntity).Value;
                    world.NewEntityWith(new CInvalidGameObjectInstanceID { Value = deadGoID });
                    world.DelEntity(deadEntity);
                }

                world.DelEntity(entity);
            }
        }

        private void HandleOnDeathEvents(int entity)
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