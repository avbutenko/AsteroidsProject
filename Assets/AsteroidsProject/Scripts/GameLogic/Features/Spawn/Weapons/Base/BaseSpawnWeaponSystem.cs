using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public abstract class BaseSpawnWeaponSystem<TRequest, TWeaponType> : IEcsRunSystem
        where TRequest : struct, IHaveConfigAddress
        where TWeaponType : struct, IHaveLinkedEntity
    {
        private readonly IConfigProvider configProvider;
        protected IActiveGOMappingService activeGOMappingService;

        public BaseSpawnWeaponSystem(IConfigProvider configProvider, IActiveGOMappingService activeGOMappingService)
        {
            this.configProvider = configProvider;
            this.activeGOMappingService = activeGOMappingService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TRequest>()
                              .End();

            var requestPool = world.GetPool<TRequest>();
            var linkToGameObjectPool = world.GetPool<CGameObjectInstanceID>();

            foreach (var entity in filter)
            {
                if (!linkToGameObjectPool.Has(entity)) return;

                var weaponConfig = requestPool.Get(entity).ConfigAddress;
                Spawn(world, entity, weaponConfig);
                requestPool.Del(entity);
            }
        }

        protected abstract Transform GetWeaponSlot(EcsWorld world, int entity);

        private async void Spawn(EcsWorld world, int ownerEntity, string configAddress)
        {
            var components = await GetComponents(world, ownerEntity, configAddress);
            var weaponEntity = world.NewEntityWithRawComponents(components);
            Link(world, ownerEntity, weaponEntity);
        }

        private async Task<List<object>> GetComponents(EcsWorld world, int ownerEntity, string configAddress)
        {
            var configComponentList = await configProvider.Load<ComponentList>(configAddress);
            var components = new List<object>();
            components.AddRange(configComponentList.Components);
            components.Add(new CPosition { Value = Vector2.zero });
            components.Add(new CRotation { Value = Quaternion.identity });
            components.Add(new CParent { Value = GetWeaponSlot(world, ownerEntity) });
            components.Add(new COwnerEntity { Value = world.PackEntity(ownerEntity) });
            return components;
        }

        private void Link(EcsWorld world, int owner, int weapon)
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