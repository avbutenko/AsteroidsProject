using AsteroidsProject.GameLogic.Ecs;
using AsteroidsProject.GameLogic.Features.Common;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.GameLogic.Features.Rotation;
using AsteroidsProject.GameLogic.Features.Teleportation;
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
            world.AddComponentToEntity(entity, new TeleportableTag());
            world.AddComponentToEntity(entity, new Position { Value = Vector2.zero });
            world.AddComponentToEntity(entity, new Rotation.Rotation { Value = Quaternion.identity });
            world.AddComponentToEntity(entity, new Scale.Scale { Value = Vector2.one[0] });
            world.AddComponentToEntity(entity, new Velocity { Value = Vector2.zero });
            world.AddComponentToEntity(entity, new ForwardAcceleration { Value = 5f });
            world.AddComponentToEntity(entity, new Deacceleration { Value = -1f });
            world.AddComponentToEntity(entity, new RotationSpeed { Value = 120 });

            var view = await InstantiateViewAsync();
            LinkViewAndEntity(world, entity, view);
        }

        private async Task<IGameplayObjectView> InstantiateViewAsync()
        {
            var instance = await factory.InstantiateAsync("Player/Prefabs/Player.prefab", Vector2.zero, Quaternion.identity, null);
            return instance.GetComponent<IGameplayObjectView>();
        }

        private void LinkViewAndEntity(EcsWorld world, int entity, IGameplayObjectView view)
        {
            world.AddComponentToEntity(entity, new GameplayObjectViewComponent { View = view });
        }
    }
}
