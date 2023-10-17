using Assets.AsteroidsProject.Scripts.Configs;
using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnBullet
{
    public class SpawnBulletSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;

        private ComponentListConfig config;

        public SpawnBulletSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<ComponentListConfig>(gameConfig.BulletConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CSpawnBulletRequest>().End();
            var requestPool = world.GetPool<CSpawnBulletRequest>();

            foreach (var entity in filter)
            {
                ref var spawnInfo = ref requestPool.Get(entity).SpawnInfo;

                Spawn(world, spawnInfo);
                world.DelEntity(entity);
            }
        }

        private void Spawn(EcsWorld world, SpawnInfo spawnInfo)
        {
            var entity = world.NewEntity();

            foreach (var component in config.Components)
            {
                world.AddRawComponentToEntity(entity, component);
            }

            world.AddComponentToEntity(entity, new CPosition { Value = spawnInfo.Position });
            world.AddComponentToEntity(entity, new CRotation { Value = spawnInfo.Rotation });

            AdoptVelocity(world, spawnInfo, entity);
        }

        private void AdoptVelocity(EcsWorld world, SpawnInfo spawnInfo, int entity)
        {
            var velocityPool = world.GetPool<CVelocity>();
            ref var velocity = ref velocityPool.Get(entity).Value;
            velocity = (Vector2)(spawnInfo.Rotation * velocity);
        }
    }
}