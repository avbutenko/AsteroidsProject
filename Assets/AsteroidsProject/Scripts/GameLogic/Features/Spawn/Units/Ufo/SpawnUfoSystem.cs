using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnUfoSystem : IEcsInitSystem, IEcsRunSystem, IEcsPostDestroySystem
    {
        private readonly IGameConfigProvider configProvider;
        private readonly IConfigLoader configLoader;
        private readonly ITimeService timeService;
        private SpawnConfig spawnconfig;
        private GameSceneConfig gameSceneConfig;
        private float timeToNextSpawn;
        private EcsWorld world;
        private GameObject parentGO;

        public SpawnUfoSystem(IGameConfigProvider configProvider, IConfigLoader configLoader, ITimeService timeService)
        {
            this.configProvider = configProvider;
            this.configLoader = configLoader;
            this.timeService = timeService;
        }

        public async void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            parentGO = new GameObject("UFOs");
            gameSceneConfig = await configLoader.Load<GameSceneConfig>(configProvider.GameConfig.ScenesConfig.GameSceneConfigLabel);
            spawnconfig = await configLoader.Load<SpawnConfig>(gameSceneConfig.UfoSpawnConfigLabel);
        }

        public void Run(IEcsSystems systems)
        {
            if (spawnconfig == null) return;

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0)
            {
                timeToNextSpawn = spawnconfig.SpawnTime;

                Spawn(gameSceneConfig.UfoConfigLabel);
            }
        }

        private async void Spawn(string config)
        {
            var componentList = await configLoader.Load<ComponentList>(config);
            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CParent { Value = parentGO.transform });
            world.AddComponentToEntity(entity, new CRotation { Value = Quaternion.identity });
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }

        public void PostDestroy(IEcsSystems systems)
        {
            Object.Destroy(parentGO);
        }
    }
}