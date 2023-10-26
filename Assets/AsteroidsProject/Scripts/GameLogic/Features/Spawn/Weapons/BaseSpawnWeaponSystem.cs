using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public abstract class BaseSpawnWeaponSystem<TRequest, TWeaponType> : IEcsRunSystem
        where TWeaponType : struct, IHaveLinkedEntity
        where TRequest : struct, IHaveConfigAddress
    {
        private readonly IConfigProvider configProvider;

        public BaseSpawnWeaponSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<TRequest>().End();
            var requestPool = world.GetPool<TRequest>();
            var linkToGameObjectPool = world.GetPool<CGameObject>();

            foreach (var entity in filter)
            {
                if (!linkToGameObjectPool.Has(entity)) return;

                ref var request = ref requestPool.Get(entity);
                var weaponConfig = request.ConfigAddress;
                Spawn(world, entity, weaponConfig);
                requestPool.Del(entity);
            }
        }

        protected abstract Transform GetWeaponSlot(EcsWorld world, int entity);

        private async void Spawn(EcsWorld world, int ownerEntity, string configAddress)
        {
            var components = await GetComponents(world, ownerEntity, configAddress);
            var weaponEntity = world.NewEntityWithComponents(components);
            Link(world, ownerEntity, weaponEntity);
        }

        private async Task<List<object>> GetComponents(EcsWorld world, int ownerEntity, string configAddress)
        {
            var configComponentList = await configProvider.Load<ComponentList>(configAddress);
            var components = configComponentList.Components;
            var runtimeComponents = GetRuntimeComponents(world, ownerEntity);
            components.AddRange(runtimeComponents);
            return components;
        }

        private List<object> GetRuntimeComponents(EcsWorld world, int ownerEntity)
        {
            return new List<object>
            {
                new CPosition { Value = Vector2.zero },
                new CRotation { Value = Quaternion.identity },
                new CParent { Value = GetWeaponSlot(world, ownerEntity) },
                new COwnerEntity { Value = world.PackEntity(ownerEntity) }
            };
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