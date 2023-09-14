using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SpawnPlayer
{
    public class SpawnPlayerSystem : IEcsInitSystem
    {
        private readonly ISceneData sceneData;

        public SpawnPlayerSystem(ISceneData sceneData)
        {
            this.sceneData = sceneData;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            world.NewEntityWith(new SpawnPrefab
            {
                PrefabAddress = "Player/Prefabs/Player.prefab",
                Position = sceneData.SpawnPlayerPosition.position,
                Rotation = sceneData.SpawnPlayerPosition.rotation,
                Parent = null
            });
        }
    }
}
