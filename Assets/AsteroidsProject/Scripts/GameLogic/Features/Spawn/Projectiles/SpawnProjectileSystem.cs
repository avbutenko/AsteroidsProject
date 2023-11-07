using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace AsteroidsProject.GameLogic.Features.Spawn.Projectiles
{
    public class SpawnProjectileSystem : IEcsRunSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;
        private readonly IActiveGOMappingService activeGOMappingService;

        public SpawnProjectileSystem(IConfigProvider configProvider, ISceneData sceneData, IActiveGOMappingService activeGOMappingService)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CSpawnProjectileRequest>()
                              .Inc<CGameObjectInstanceID>()
                              .End();

            var requestPool = world.GetPool<CSpawnProjectileRequest>();
            var goIDPool = world.GetPool<CGameObjectInstanceID>();

            foreach (var entity in filter)
            {
                ref var goID = ref goIDPool.Get(entity).Value;
                if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return;
                var shootingPoint = goLink as IHaveShootingPoint;

                ref var config = ref requestPool.Get(entity).Config;
                Spawn(world, shootingPoint.ShootingPoint, config);
                requestPool.Del(entity);
            }
        }

        private async void Spawn(EcsWorld world, Transform shootingPoint, string config)
        {
            var components = await GetComponents(shootingPoint, config);
            var projectileEntity = world.NewEntityWithRawComponents(components);
            world.AddComponentToEntity(projectileEntity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(projectileEntity) });
            AdoptVelocity(world, shootingPoint, projectileEntity);
        }

        private async Task<List<object>> GetComponents(Transform shootingPoint, string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var components = new List<object>();
            components.AddRange(componentList.Components);
            components.Add(new CPosition { Value = shootingPoint.position });
            components.Add(new CRotation { Value = shootingPoint.rotation });
            components.Add(new CParent { Value = sceneData.ProjectilePool });
            return components;
        }

        private void AdoptVelocity(EcsWorld world, Transform shootingPoint, int projectileEntity)
        {
            var velocityPool = world.GetPool<CVelocity>();
            if (velocityPool.Has(projectileEntity))
            {
                ref var velocity = ref velocityPool.Get(projectileEntity).Value;
                velocity = (Vector2)(shootingPoint.rotation * velocity);
            }
        }
    }
}