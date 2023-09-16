using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnPlayer
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;

        public SpawnPlayerSystem(ISceneData sceneData, IConfigProvider configProvider)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var spawnConfig = await configProvider.Load<PlayerConfig>("Configs/PlayerConfig.json");

            world.NewEntityWith(new SpawnPrefab
            {
                PrefabAddress = spawnConfig.PrefabAddresses[0],
                Position = sceneData.SpawnPlayerPosition.position,
                Rotation = sceneData.SpawnPlayerPosition.rotation,
                Parent = null
            });
        }
    }
}
