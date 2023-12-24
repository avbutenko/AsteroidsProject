using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly IGameSceneData sceneData;
        private readonly IGameConfigProvider configProvider;
        private readonly IConfigLoader configLoader;
        private EcsWorld world;

        public SpawnPlayerSystem(IGameSceneData sceneData, IGameConfigProvider configProvider, IConfigLoader configLoader)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
            this.configLoader = configLoader;
        }

        public async void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            var gameSceneConfig = await configLoader.Load<GameSceneConfig>(configProvider.GameConfig.ScenesConfig.GameSceneConfigLabel);
            Spawn(gameSceneConfig.PlayerConfigPath);
        }

        private async void Spawn(string config)
        {
            var componentList = await configLoader.Load<ComponentList>(config);
            var components = componentList.Components;
            components.Add(new CPosition { Value = sceneData.SpawnPlayerPoint.position });
            components.Add(new CRotation { Value = sceneData.SpawnPlayerPoint.rotation });

            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }
    }
}