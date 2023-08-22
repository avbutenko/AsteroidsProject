using AsteroidsProject.GameLogic.Ecs;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Views;
using Leopotam.EcsLite;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class PlayerSpawnSystem : IEcsInitSystem
    {
        private readonly IGameplayObjectViewFactory factory;

        public PlayerSpawnSystem(IGameplayObjectViewFactory factory)
        {
            this.factory = factory;
        }

        public async void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();

            world.AddComponentToEntity(entity, new PlayerTag());
            AddMovementFeature(world, entity);
            AddRotationFeature(world, entity);

            var view = await InstantiateViewAsync();
            LinkViewAndEntity(world, entity, view);
        }

        private void AddMovementFeature(EcsWorld world, int entity)
        {
            world.AddComponentToEntity(entity, new MovementSpeed { Value = 5 });
            world.AddComponentToEntity(entity, new MovementMaxSpeed { Value = 5 });
            world.AddComponentToEntity(entity, new MovementCurrentSpeed { Value = 0 });
            world.AddComponentToEntity(entity, new MovementDirection { Value = Vector2.up });
            world.AddComponentToEntity(entity, new MovementAccelerationModifier { Value = 2 });
            world.AddComponentToEntity(entity, new MovementInertiaModifier { Value = 3 });
            world.AddComponentToEntity(entity, new Position { Value = Vector2.zero });
        }

        private void AddRotationFeature(EcsWorld world, int entity)
        {
            world.AddComponentToEntity(entity, new RotationSpeed { Value = 120 });
            world.AddComponentToEntity(entity, new Rotation.Rotation { Value = Quaternion.identity });
        }

        private async Task<IGameplayObjectView> InstantiateViewAsync()
        {
            var instance = await factory.InstantiateAsync("Player/Prefabs/Player.prefab", Vector3.zero, Quaternion.identity, null);
            return instance.GetComponent<IGameplayObjectView>();
        }

        private void LinkViewAndEntity(EcsWorld world, int entity, IGameplayObjectView view)
        {
            world.AddComponentToEntity(entity, new GameplayObjectViewComponent { View = view });
        }
    }
}
