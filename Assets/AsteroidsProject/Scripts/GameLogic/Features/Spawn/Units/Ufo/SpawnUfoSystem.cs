using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Units.Ufo
{
    public class SpawnUfoSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;
        private readonly ITimeService timeService;
        private SpawnConfig spawnconfig;
        private GameConfig gameConfig;
        private float timeToNextSpawn;
        private EcsWorld world;

        public SpawnUfoSystem(IConfigProvider configProvider, ITimeService timeService, ISceneData sceneData)
        {
            this.configProvider = configProvider;
            this.timeService = timeService;
            this.sceneData = sceneData;
        }

        public async void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            gameConfig = await configProvider.Load<GameConfig>(configProvider.GameConfigPath);
            spawnconfig = await configProvider.Load<SpawnConfig>(gameConfig.UfoSpawnConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            if (spawnconfig == null) return;

            timeToNextSpawn -= timeService.DeltaTime;

            if (timeToNextSpawn < 0)
            {
                timeToNextSpawn = spawnconfig.SpawnTime;

                Spawn(gameConfig.UfoConfigPath);
            }
        }

        private async void Spawn(string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CParent { Value = sceneData.UfoPool });
            world.AddComponentToEntity(entity, new CRotation { Value = Quaternion.identity });
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }
    }
}