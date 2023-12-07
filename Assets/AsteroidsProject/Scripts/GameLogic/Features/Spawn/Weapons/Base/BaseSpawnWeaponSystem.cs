using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public abstract class BaseSpawnWeaponSystem<TRequest, TWeaponType> : IEcsInitSystem, IEcsRunSystem
        where TRequest : struct, IHaveConfigAddress
        where TWeaponType : struct, IHaveLinkedEntity
    {
        private readonly IConfigProvider configProvider;
        protected IActiveGOMappingService activeGOMappingService;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<TRequest> requestPool;
        private EcsPool<CGameObjectInstanceID> linkToGameObjectPool;
        protected EcsPool<CGameObjectInstanceID> goIDPool;

        public BaseSpawnWeaponSystem(IConfigProvider configProvider, IActiveGOMappingService activeGOMappingService)
        {
            this.configProvider = configProvider;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            filter = world.Filter<TRequest>().End();
            requestPool = world.GetPool<TRequest>();
            linkToGameObjectPool = world.GetPool<CGameObjectInstanceID>();
            goIDPool = world.GetPool<CGameObjectInstanceID>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                if (!linkToGameObjectPool.Has(entity)) return;

                var weaponConfig = requestPool.Get(entity).ConfigAddress;
                Spawn(entity, weaponConfig);
                requestPool.Del(entity);
            }
        }

        protected abstract Transform GetWeaponSlot(int entity);

        private async void Spawn(int ownerEntity, string configAddress)
        {
            var components = await GetComponents(ownerEntity, configAddress);
            var weaponEntity = world.NewEntityWithRawComponents(components);
            world.AddComponentToEntity(weaponEntity, new CSpawnedEntityEvent { PackedEntity = world.PackEntity(weaponEntity) });
            Link(ownerEntity, weaponEntity);
        }

        private async UniTask<List<object>> GetComponents(int ownerEntity, string configAddress)
        {
            var configComponentList = await configProvider.Load<ComponentList>(configAddress);
            var components = new List<object>();
            components.AddRange(configComponentList.Components);
            components.Add(new CPosition { Value = Vector2.zero });
            components.Add(new CRotation { Value = Quaternion.identity });
            components.Add(new CParent { Value = GetWeaponSlot(ownerEntity) });
            components.Add(new COwnerEntity { Value = world.PackEntity(ownerEntity) });
            return components;
        }

        private void Link(int owner, int weapon)
        {
            var weaponPackedEntity = world.PackEntity(weapon);
            var component = new TWeaponType
            {
                LinkedEntity = weaponPackedEntity
            };
            world.AddComponentToEntity(owner, component);
        }
    }
}