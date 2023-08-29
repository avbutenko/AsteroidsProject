using AsteroidsProject.GameLogic.Ecs;
using AsteroidsProject.GameLogic.Features.Movement;
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
            world.AddComponentToEntity(entity, new Velocity { Value = Vector2.zero });
            world.AddComponentToEntity(entity, new Acceleration { Value = Vector2.zero });

            world.AddComponentToEntity(entity, new GameLogic.GameplayObjectViewComponent
            {
                Position = Vector2.zero,
                Rotation = Quaternion.identity,
                Scale = 1
            });

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
