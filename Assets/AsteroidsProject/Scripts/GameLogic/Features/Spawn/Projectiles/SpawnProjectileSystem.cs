using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnProjectileSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IGameSceneData sceneData;
        private readonly IConfigLoader configProvider;
        private readonly IActiveGOMappingService activeGOMappingService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CSpawnProjectileRequest> requestPool;
        private EcsPool<CGameObjectInstanceID> goIDPool;

        public SpawnProjectileSystem(IConfigLoader configProvider, IGameSceneData sceneData, IActiveGOMappingService activeGOMappingService)
        {
            this.sceneData = sceneData;
            this.configProvider = configProvider;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CSpawnProjectileRequest>()
                          .Inc<CGameObjectInstanceID>()
                          .End();

            requestPool = world.GetPool<CSpawnProjectileRequest>();
            goIDPool = world.GetPool<CGameObjectInstanceID>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var goID = ref goIDPool.Get(entity).Value;
                if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return;
                var shootingPoint = goLink as IHaveShootingPoint;

                ref var config = ref requestPool.Get(entity).Config;
                ref var parentType = ref requestPool.Get(entity).ParentType;
                Spawn(shootingPoint.ShootingPoint, config, entity, parentType);
                requestPool.Del(entity);
            }
        }

        private async void Spawn(Transform shootingPoint, string config, int weaponEntity, ParentType parentType)
        {
            var components = await GetComponents(shootingPoint, config, parentType);
            var projectileEntity = world.NewEntityWithRawComponents(components);
            world.AddComponentToEntity(projectileEntity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(projectileEntity) });
            AdoptVelocity(shootingPoint, projectileEntity, weaponEntity);
        }

        private async UniTask<List<object>> GetComponents(Transform shootingPoint, string config, ParentType parentType)
        {
            var componentList = await configProvider.Load<ComponentList>(config);
            var components = new List<object>();
            components.AddRange(componentList.Components);

            switch (parentType)
            {
                case ParentType.Pool:
                    components.Add(new CParent { Value = sceneData.ProjectilePool });
                    components.Add(new CPosition { Value = shootingPoint.position });
                    components.Add(new CRotation { Value = shootingPoint.rotation });
                    break;
                case ParentType.OwnerEntity:
                    components.Add(new CParent { Value = shootingPoint });
                    components.Add(new CPosition { Value = Vector2.zero });
                    components.Add(new CRotation { Value = Quaternion.identity });
                    break;
            }

            return components;
        }

        private void AdoptVelocity(Transform shootingPoint, int projectileEntity, int weaponEntity)
        {
            var velocityPool = world.GetPool<CVelocity>();
            var ownerPool = world.GetPool<COwnerEntity>();

            if (velocityPool.Has(projectileEntity))
            {
                ref var velocity = ref velocityPool.Get(projectileEntity).Value;
                velocity = (Vector2)(shootingPoint.rotation * velocity);

                if (ownerPool.Has(weaponEntity))
                {
                    ref var ownerPackedEntity = ref ownerPool.Get(weaponEntity).Value;

                    if (ownerPackedEntity.Unpack(world, out int ownerEntity))
                    {
                        ref var ownerVelocity = ref velocityPool.Get(ownerEntity).Value;
                        velocity += ownerVelocity;
                    }
                }
            }
        }
    }
}