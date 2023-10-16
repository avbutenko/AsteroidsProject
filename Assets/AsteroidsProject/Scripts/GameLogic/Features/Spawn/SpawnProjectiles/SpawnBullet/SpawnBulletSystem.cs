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
        private readonly IGameObjectFactory gameObjectFactory;

        private BulletConfig config;

        public SpawnBulletSystem(IConfigProvider configProvider, IGameObjectFactory gameObjectFactory)
        {
            this.configProvider = configProvider;
            this.gameObjectFactory = gameObjectFactory;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<BulletConfig>(gameConfig.BulletConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            if (config == null)
            {
                return;
            }

            var world = systems.GetWorld();
            var filter = world.Filter<SpawnBulletRequest>().End();
            var requestPool = world.GetPool<SpawnBulletRequest>();

            foreach (var entity in filter)
            {
                ref var spawnInfo = ref requestPool.Get(entity).SpawnInfo;
                Spawn(world, spawnInfo);
                world.DelEntity(entity);
            }
        }

        private async void Spawn(EcsWorld world, SpawnInfo spawnInfo)
        {
            var position = spawnInfo.Position;
            var rotation = spawnInfo.Rotation;
            var velocityX = config.VelocityX;
            var velocityY = config.VelocityY;
            var velocityVector = (Vector2)(rotation * new Vector2(velocityX, velocityY));

            var bulletEntity = world.NewEntity();
            world.AddComponentToEntity(bulletEntity, new CBulletTag { });
            world.AddComponentToEntity(bulletEntity, new CPosition { Value = position });
            world.AddComponentToEntity(bulletEntity, new CRotation { Value = rotation });
            world.AddComponentToEntity(bulletEntity, new CVelocity { Value = velocityVector });

            var go = await gameObjectFactory.CreateAsync(new SpawnInfo
            {
                PrefabAddress = config.PrefabAddress,
                Position = position,
                Rotation = rotation,
                Parent = spawnInfo.Parent
            });

            var goLink = go.GetComponent<ILinkToGameObject>();
            goLink.Entity = world.PackEntity(bulletEntity);
            world.AddComponentToEntity(bulletEntity, new CLinkToGameObject { View = goLink });
        }
    }
}