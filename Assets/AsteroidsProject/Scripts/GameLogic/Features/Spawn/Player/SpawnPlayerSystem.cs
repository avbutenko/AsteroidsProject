using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Spawn.Player
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly IGameSceneData sceneData;
        private readonly IConfigProvider configProvider;
        private EcsWorld world;

        public SpawnPlayerSystem(IGameSceneData sceneData, IConfigProvider configProvider, IUIService uiService)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            var gameSceneConfig = await configProvider.Load<GameSceneConfig>(sceneData.SceneConfigAssetPath);
            Spawn(gameSceneConfig.PlayerConfigPath);
        }

        private async void Spawn(string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var components = componentList.Components;
            components.Add(new CPosition { Value = sceneData.SpawnPlayerPoint.position });
            components.Add(new CRotation { Value = sceneData.SpawnPlayerPoint.rotation });

            var entity = world.NewEntityWithRawComponents(componentList.Components);
            world.AddComponentToEntity(entity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(entity) });
        }
    }
}