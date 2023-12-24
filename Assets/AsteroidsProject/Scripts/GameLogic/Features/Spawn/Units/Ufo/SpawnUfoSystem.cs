using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnUfoSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameSceneData sceneData;
        private readonly IGameConfigProvider configProvider;
        private readonly IConfigLoader configLoader;
        private readonly ITimeService timeService;
        private SpawnConfig spawnconfig;
        private GameSceneConfig gameSceneConfig;
        private float timeToNextSpawn;
        private EcsWorld world;

        public SpawnUfoSystem(IGameConfigProvider configProvider, IConfigLoader configLoader,
            ITimeService timeService, IGameSceneData sceneData)
        {
            this.configProvider = configProvider;
            this.configLoader = configLoader;
            this.timeService = timeService;
            this.sceneData = sceneData;
        }

        public async void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            gameSceneConfig = await configLoader.Load<GameSceneConfig>(configProvider.GameConfig.ScenesConfig.GameSceneConfigLabel);
            spawnconfig = await configLoader.Load<SpawnConfig>(gameSceneConfig.UfoSpawnConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            if (spawnconfig == null) return;

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0)
            {
                timeToNextSpawn = spawnconfig.SpawnTime;

                Spawn(gameSceneConfig.UfoConfigPath);
            }
        }

        private async void Spawn(string config)
        {
            var componentList = await configLoader.Load<ComponentList>(config);
            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CParent { Value = sceneData.UfoPool });
            world.AddComponentToEntity(entity, new CRotation { Value = Quaternion.identity });
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }
    }
}