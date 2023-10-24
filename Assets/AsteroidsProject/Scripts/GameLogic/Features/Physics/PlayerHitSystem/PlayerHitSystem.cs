using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.PlayerHitSystem
{
    public class PlayerHitSystem : IEcsRunSystem
    {
        private readonly IActiveGameObjectMapService activeGameObjectPool;

        public PlayerHitSystem(IActiveGameObjectMapService activeGameObjectPool)
        {
            this.activeGameObjectPool = activeGameObjectPool;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<OnTriggerEnter2DEvent>().End();

            var destroyRequestPool = world.GetPool<CDestructionRequest>();
            var eventPool = world.GetPool<OnTriggerEnter2DEvent>();

            foreach (var entity in filter)
            {
                ref var senderGameObject = ref eventPool.Get(entity).senderGameObject;
                ref var collisionGameObject = ref eventPool.Get(entity).collider2D;

                var senderEntity = GetEntity(world, senderGameObject);
                var collisionEntity = GetEntity(world, collisionGameObject.gameObject);

                if (!destroyRequestPool.Has(collisionEntity))
                {
                    world.AddComponentToEntity(collisionEntity, new CDestructionRequest { });
                }

                eventPool.Del(entity);
            }
        }

        private int GetEntity(EcsWorld world, GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out IGameObject link))
            {
                throw new InvalidOperationException($"No IGameObject attached to: {gameObject}");
            }

            if (!activeGameObjectPool.Has(link))
            {
                throw new InvalidOperationException($"No entity assigned to: {gameObject} in Pool");
            }
            else
            {
                var packedEntity = activeGameObjectPool.GetValuePair(link).PackedEntity;
                if (packedEntity.Unpack(world, out int entity))
                {
                    return entity;
                }
                else
                {
                    throw new InvalidOperationException($"Assigned to: {gameObject} entity is not alive");
                }
            }
        }
    }
}
