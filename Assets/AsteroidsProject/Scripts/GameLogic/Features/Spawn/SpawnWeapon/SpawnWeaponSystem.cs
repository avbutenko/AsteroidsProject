using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnWeapon
{
    public class SpawnWeaponSystem : BaseSpawnSystem, IEcsRunSystem
    {
        public SpawnWeaponSystem(IGameObjectFactory factory) : base(factory) { }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<SpawnWeaponRequest>().End();

            var requestPool = world.GetPool<SpawnWeaponRequest>();
            var primaryWeaponPool = world.GetPool<CPrimaryWeapon>();
            var secondaryWeaponPool = world.GetPool<CSecondaryWeapon>();

            foreach (var entity in filter)
            {
                ref var weaponInfo = ref requestPool.Get(entity).Info;

                foreach (var info in weaponInfo)
                {
                    SetWeapon(entity, info, world, primaryWeaponPool, secondaryWeaponPool);
                }

                requestPool.Del(entity);
            }
        }

        private async void SetWeapon(int ownerEntity, SpawnWeaponInfo spawnInfo, EcsWorld world,
            EcsPool<CPrimaryWeapon> primaryWeaponPool, EcsPool<CSecondaryWeapon> secondaryWeaponPool)
        {
            var newEntityWithGameObject = await Spawn(new SpawnInfo
            {
                PrefabAddress = spawnInfo.PrefabAddress,
                Position = Vector2.zero,
                Rotation = Quaternion.identity,
                Parent = spawnInfo.WeaponSlot,
                World = world,
                OwnerEntity = world.PackEntity(ownerEntity)
            });

            var weaponPackedEntity = world.PackEntity(newEntityWithGameObject.Entity);

            switch (spawnInfo.WeaponType)
            {
                case WeaponType.Primary:
                    primaryWeaponPool.Add(ownerEntity).WeaponEntity = weaponPackedEntity;
                    break;
                case WeaponType.Secondary:
                    secondaryWeaponPool.Add(ownerEntity).WeaponEntity = weaponPackedEntity;
                    break;
                default:
                    break;
            }
        }
    }
}