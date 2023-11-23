using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Obstacles.AsteroidFragment
{
    public class SpawnAsteroidFragmentSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CSpawnAsteroidFragmentsRequest> requestPool;

        public SpawnAsteroidFragmentSystem(ISceneData sceneData, IConfigProvider configProvider)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<CSpawnAsteroidFragmentsRequest>().End();
            requestPool = world.GetPool<CSpawnAsteroidFragmentsRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var num = ref requestPool.Get(entity).Amount;
                ref var config = ref requestPool.Get(entity).Config;
                ref var spawnPosition = ref requestPool.Get(entity).SpawnPosition;

                while (num > 0)
                {
                    num--;
                    Spawn(spawnPosition, config);
                }

                world.DelEntity(entity);
            }
        }

        private async void Spawn(Vector2 spawnPosition, string config)
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