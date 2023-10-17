using Assets.AsteroidsProject.Scripts.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.PrimaryWeapon
{
    public class SpawnPrimaryWeaponSystem : IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;

        public SpawnPrimaryWeaponSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<CSpawnPrimaryWeaponRequest>().End();

            var requestPool = world.GetPool<CSpawnPrimaryWeaponRequest>();
            var linkToGameObjectPool = world.GetPool<CLinkToGameObject>();

            foreach (var entity in filter)
            {
                if (!linkToGameObjectPool.Has(entity)) return;

                ref var weaponConfig = ref requestPool.Get(entity).ConfigAddress;
                ref var view = ref linkToGameObjectPool.Get(entity).View;
                var slot = (view as IHavePrimaryWeapon).PrimaryWeaponSlot;

                Spawn(weaponConfig, slot, world, entity);
                requestPool.Del(entity);
            }
        }

        private async void Spawn(string configAddress, Transform slot, EcsWorld world, int owner)
        {
            var config = await configProvider.Load<ComponentListConfig>(configAddress);

            var weaponEntity = CreateWeaponEntity(world, config, slot);

            LinkWeaponEntityWithOwner(world, owner, weaponEntity);
        }

        private int CreateWeaponEntity(EcsWorld world, ComponentListConfig config, Transform slot)
        {
            var weaponEntity = world.NewEntity();

            foreach (var component in config.Components)
            {
                world.AddRawComponentToEntity(weaponEntity, component);
            }

            world.AddComponentToEntity(weaponEntity, new CPosition { Value = Vector2.zero });
            world.AddComponentToEntity(weaponEntity, new CRotation { Value = Quaternion.identity });
            world.AddComponentToEntity(weaponEntity, new CParent { Value = slot });

            return weaponEntity;
        }

        private void LinkWeaponEntityWithOwner(EcsWorld world, int owner, int weapon)
        {
            var ownerPackedEntity = world.PackEntity(owner);
            world.AddComponentToEntity(weapon, new COwnerEntity { Value = ownerPackedEntity });

            var weaponPackedEntity = world.PackEntity(weapon);
            world.AddComponentToEntity(owner, new CPrimaryWeapon { WeaponEntity = weaponPackedEntity });
        }
    }
}