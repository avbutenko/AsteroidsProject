using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Events
{
    public class OnCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IActiveGOMappingService activeGOMappingService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<OnCollisionEnter2DEvent> collisionPool;
        private EcsPool<COnCollision> onCollisionPool;

        public OnCollisionSystem(IActiveGOMappingService activeGOMappingService)
        {
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<OnCollisionEnter2DEvent>().End();
            collisionPool = world.GetPool<OnCollisionEnter2DEvent>();
            onCollisionPool = world.GetPool<COnCollision>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var senderGO = ref collisionPool.Get(entity).senderGameObject;

                if (senderGO == null || !senderGO.activeSelf)
                {
                    collisionPool.Del(entity);
                    continue;
                }

                var senderGOID = senderGO.GetInstanceID();

                if (!TryUnpack(world, senderGOID, out var senderEntity))
                {
                    collisionPool.Del(entity);
                    continue;
                }

                ref var collider = ref collisionPool.Get(entity).collider2D;

                if (collider == null || !collider.gameObject.activeSelf)
                {
                    collisionPool.Del(entity);
                    continue;
                }

                var collisionGOID = collider.gameObject.GetInstanceID();

                if (!TryUnpack(world, collisionGOID, out var collisionEntity))
                {
                    collisionPool.Del(entity);
                    continue;
                }

                if (onCollisionPool.Has(senderEntity.Value))
                {
                    ref var paramList = ref onCollisionPool.Get(senderEntity.Value).Params;

                    foreach (var param in paramList)
                    {
                        foreach (var tag in param.RelevantForCollisionTags)
                        {
                            var relavantForCollisionTagPool = world.GetPoolByType(tag.GetType());

                            if (relavantForCollisionTagPool != null && relavantForCollisionTagPool.Has(collisionEntity.Value))
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
