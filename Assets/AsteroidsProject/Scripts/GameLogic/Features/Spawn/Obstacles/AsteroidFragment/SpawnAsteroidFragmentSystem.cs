using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Obstacles.AsteroidFragment
{
    public class SpawnAsteroidFragmentSystem : IEcsRunSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;

        public SpawnAsteroidFragmentSystem(ISceneData sceneData, IConfigProvider configProvider)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CSpawnAsteroidFragmentsRequest>().End();
            var requestPool = world.GetPool<CSpawnAsteroidFragmentsRequest>();

            foreach (var entity in filter)
            {
                ref var num = ref requestPool.Get(entity).Amount;
                ref var config = ref requestPool.Get(entity).Config;
                ref var spawnPosition = ref requestPool.Get(entity).SpawnPosition;

                while (num > 0)
                {
                    num--;
                    Spawn(world, spawnPosition, config);
                }

                world.DelEntity(entity);
            }
        }

        private async void Spawn(EcsWorld world, Vector2 spawnPosition, string config)
        {
            var components = await GetComponents(spawnPosition, config);
            var fragmentEntity = world.NewEntityWithRawComponents(components);
            world.AddComponentToEntity(fragmentEntity, new CParent { Value = sceneData.AsteroidsPool });
            world.AddComponentToEntity(fragmentEntity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(fragmentEntity) });
        }

        private async Task<List<object>> GetComponents(Vector2 spawnPosition, string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var components = new List<object>();
            components.AddRange(componentList.Components);
            components.Add(new CPosition { Value = spawnPosition });
            return components;
        }
    }
}