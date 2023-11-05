using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events.OnCollision
{
    public class CollisionSystem : IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;

        public CollisionSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<OnCollisionEnter2DEvent>().End();
            var collisionPool = world.GetPool<OnCollisionEnter2DEvent>();
            var onCollisionPool = world.GetPool<COnCollision>();

            foreach (var entity in filter)
            {
                ref var senderGO = ref collisionPool.Get(entity).senderGameObject;
                var senderGOID = senderGO.GetInstanceID();
                if (!TryUnpack(world, senderGOID, out var senderEntity)) return;

                ref var collider = ref collisionPool.Get(entity).collider2D;
                var collisionGOID = collider.gameObject.GetInstanceID();
                if (!TryUnpack(world, collisionGOID, out var collisionEntity)) return;


                if (onCollisionPool.Has(senderEntity.Value))
                {
                    ref var paramList = ref onCollisionPool.Get(senderEntity.Value).Params;

                    foreach (var param in paramList)
                    {
                        foreach (var tag in param.RelevantForCollisionTags)
                        {
                            var relavantForCollisionTagPool = world.GetPoolByType(tag.GetType());

                            if (relavantForCollisionTagPool.Has(collisionEntity.Value))
                            {
                                world.AddRawComponentsToEntity(senderEntity.Value, param.AddToSelfComponents);
                                world.AddRawComponentsToEntity(collisionEntity.Value, param.AddToCollidedObjectComponents);
                            }
                        }
                    }
                }

                collisionPool.Del(entity);
            }
        }

        private bool TryUnpack(EcsWorld world, int goID, out int? entity)
        {
            if (!activeGOMappingService.TryGetEntity(goID, out var packedEntity))
            {
                entity = null;
                return false;
            }

            if (!packedEntity.HasValue)
            {
                entity = null;
                return false;
            }

            if (!packedEntity.Value.Unpack(world, out int unpackedEntity))
            {
                entity = null;
                return false;
            }

            entity = unpackedEntity;
            return true;
        }
    }
}
