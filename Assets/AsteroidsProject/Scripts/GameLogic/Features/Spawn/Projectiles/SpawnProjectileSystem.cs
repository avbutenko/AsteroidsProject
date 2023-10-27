using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Projectiles
{
    public class SpawnProjectileSystem : IEcsRunSystem
    {
        private readonly ISceneData sceneData;
        private readonly IConfigProvider configProvider;

        public SpawnProjectileSystem(IConfigProvider configProvider, ISceneData sceneData)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var filter = world.Filter<CSpawnProjectileRequest>()
                              .Inc<CProjectileConfig>()
                              .End();

            var requestPool = world.GetPool<CSpawnProjectileRequest>();
            var configPool = world.GetPool<CProjectileConfig>();

            foreach (var entity in filter)
            {
                ref var shootingPoint = ref requestPool.Get(entity).ShootingPoint;
                ref var config = ref configPool.Get(entity).Config;
                Spawn(world, shootingPoint, config);
                requestPool.Del(entity);
            }
        }

        private async void Spawn(EcsWorld world, IHaveShootingPoint shootingPoint, string config)
        {
            var components = await GetComponents(shootingPoint, config);
            var projectileEntity = world.NewEntityWithRawComponents(components);
            AdoptVelocity(world, shootingPoint, projectileEntity);
        }

        private async Task<List<object>> GetComponents(IHaveShootingPoint shootingPoint, string config)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var components = new List<object>();
            components.AddRange(componentList.Components);
            components.Add(new CPosition { Value = shootingPoint.ShootingPoint.position });
            components.Add(new CRotation { Value = shootingPoint.ShootingPoint.rotation });
            components.Add(new CParent { Value = sceneData.ProjectilePool });
            return components;
        }

        private void AdoptVelocity(EcsWorld world, IHaveShootingPoint shootingPoint, int projectileEntity)
        {
            var velocityPool = world.GetPool<CVelocity>();
            if (velocityPool.Has(projectileEntity))
            {
                ref var velocity = ref velocityPool.Get(projectileEntity).Value;
                velocity = (Vector2)(shootingPoint.ShootingPoint.rotation * velocity);
            }
        }
    }
}