using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnPrefab
{
    public class SpawnPrefabSystem : IEcsRunSystem
    {
        private readonly IGameObjectFactory factory;

        public SpawnPrefabSystem(IGameObjectFactory factory)
        {
            this.factory = factory;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<Core.SpawnPrefab>().End();
            var spawnPrefabPool = world.GetPool<Core.SpawnPrefab>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref spawnPrefabPool.Get(entity).PrefabAddress;
                ref var position = ref spawnPrefabPool.Get(entity).Position;
                ref var rotation = ref spawnPrefabPool.Get(entity).Rotation;
                ref var parent = ref spawnPrefabPool.Get(entity).Parent;
                ref var owner = ref spawnPrefabPool.Get(entity).OwnerEntity;

                SpawnAndLink(prefabAddress, position, rotation, parent, world, owner);

                world.DelEntity(entity);
            }
        }

        private async void SpawnAndLink(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parent, EcsWorld world, EcsPackedEntity ecsOwnerPackedEntity)
        {
            var entityWithGameObject = await factory.InstantiateAsync(prefabAddress, position, rotation, parent, world);

            var gameObject = entityWithGameObject.GameObject.GetComponent<IGameObject>();

            world.AddComponentToEntity(entityWithGameObject.Entity, new LinkToGameObject { View = gameObject });
            world.AddComponentToEntity(entityWithGameObject.Entity, new Position { Value = position });
            world.AddComponentToEntity(entityWithGameObject.Entity, new Rotation { Value = rotation });

            if (ecsOwnerPackedEntity.Unpack(world, out int entity))
            {
                world.AddComponentToEntity(entityWithGameObject.Entity, new OwnerEntity { Value = ecsOwnerPackedEntity });
            }
        }
    }
}