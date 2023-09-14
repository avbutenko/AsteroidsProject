using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnPrefab
{
    public class SpawnPrefabSystem : IEcsRunSystem
    {
        private readonly IGameplayObjectViewFactory factory;

        public SpawnPrefabSystem(IGameplayObjectViewFactory factory)
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

                SpawnAndLink(prefabAddress, position, rotation, parent, world);

                world.DelEntity(entity);
            }
        }

        private async void SpawnAndLink(string prefabAddress,
            Vector2 position, Quaternion rotation, Transform parentTransform, EcsWorld world)
        {
            var entityLinkedToView = await factory.InstantiateAsync(prefabAddress, position, rotation, parentTransform, world);

            var view = entityLinkedToView.View.GetComponent<ILinkToGameplayObjectView>();
            world.AddComponentToEntity(entityLinkedToView.Entity, new LinkToGameplayObjectView { View = view });
            world.AddComponentToEntity(entityLinkedToView.Entity, new Position { Value = position });
            world.AddComponentToEntity(entityLinkedToView.Entity, new Rotation { Value = rotation });
        }
    }
}