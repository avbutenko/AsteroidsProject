using AsteroidsProject.GameLogic.Ecs;
using AsteroidsProject.GameLogic.Features.Common;
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
            world.AddComponentToEntity(entity, new Position { Value = Vector3.zero });
            world.AddComponentToEntity(entity, new Rotation.Rotation { Value = Quaternion.identity });
            world.AddComponentToEntity(entity, new Velocity { Value = Vector3.zero });
            world.AddComponentToEntity(entity, new AccelerationModifier { Value = Vector3.up });
            world.AddComponentToEntity(entity, new InertionModifier { Value = Vector3.down });
            world.AddComponentToEntity(entity, new RotationSpeed { Value = 120 });

            var view = await InstantiateViewAsync();
            LinkViewAndEntity(world, entity, view);
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
