using AsteroidsProject.Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.Features.Spawn
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
            await factory.InstantiateAsync("Player/Prefabs/Player.prefab", Vector2.zero, Quaternion.identity, null, world);
        }
    }
}
