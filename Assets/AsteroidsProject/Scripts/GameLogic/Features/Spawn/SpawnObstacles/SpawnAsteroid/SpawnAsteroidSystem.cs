using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnAsteroid
{
    public class SpawnAsteroidSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private readonly ILevelService level;
        private readonly ISceneData sceneData;
        private readonly IGameObjectFactory gameObjectFactory;

        private AsteroidConfig config;

        public SpawnAsteroidSystem(IConfigProvider configProvider, ILevelService level, ISceneData sceneData,
            IGameObjectFactory gameObjectFactory)
        {
            this.configProvider = configProvider;
            this.level = level;
            this.sceneData = sceneData;
            this.gameObjectFactory = gameObjectFactory;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<AsteroidConfig>(gameConfig.AsteroidConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<SpawnAsteroidRequest>().End();

            foreach (var entity in filter)
            {
                world.DelEntity(entity);
                Spawn(world);
            }
        }

        private async void Spawn(EcsWorld world)
        {
            var position = level.GetRandomPosition();
            var rotation = Quaternion.identity;
            var velocityX = Random.Range(config.VelocityX[0], config.VelocityX[1]);
            var velocityY = Random.Range(config.VelocityY[0], config.VelocityY[1]);
            var rotationDirection = Random.Range(config.RotationDirection[0], config.RotationDirection[1]);
            var rotationSpeed = Random.Range(config.RotationSpeed[0], config.RotationSpeed[1]);

            var go = await gameObjectFactory.CreateAsync(new SpawnInfo
            {
                PrefabAddress = config.PrefabAddress,
                Position = position,
                Rotation = rotation,
                Parent = sceneData.AsteroidsPool
            });

            var asteroidEntity = world.NewEntity();
            world.AddComponentToEntity(asteroidEntity, new AsteroidTag { });
            world.AddComponentToEntity(asteroidEntity, new TeleportableTag { });
            world.AddComponentToEntity(asteroidEntity, new Position { Value = position });
            world.AddComponentToEntity(asteroidEntity, new Rotation { Value = rotation });
            world.AddComponentToEntity(asteroidEntity, new Velocity { Value = new Vector2(velocityX, velocityY) });
            world.AddComponentToEntity(asteroidEntity, new RotationDirection { Value = rotationDirection });
            world.AddComponentToEntity(asteroidEntity, new RotationSpeed { Value = rotationSpeed });

            var goLink = go.GetComponent<ILinkToGameObject>();
            goLink.Entity = world.PackEntity(asteroidEntity);
            world.AddComponentToEntity(asteroidEntity, new LinkToGameObject { View = goLink });
        }
    }
}