using AsteroidsProject.GameLogic.Extensions;
using AsteroidsProject.GameLogic.Features.Movement;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Views;
using Leopotam.EcsLite;
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

            world.AddComponent(entity, new PlayerTag());
            world.AddComponent(entity, new MovementSpeed { Value = 5 });
            world.AddComponent(entity, new RotationSpeed { Value = 120 });
            world.AddComponent(entity, new Position { Value = Vector3.one });
            world.AddComponent(entity, new Rotation { Value = Quaternion.identity });

            var view = await factory.InstantiateAsync("Player/Prefabs/Player.prefab", Vector3.zero, Quaternion.identity, null);
            var link = view.GetComponent<IGameplayObjectView>();
            world.AddComponent(entity, new GameplayObjectViewComponent { View = link });
        }
    }
}
