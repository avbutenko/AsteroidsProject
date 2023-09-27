using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.PrimaryWeapon
{
    public class SpawnPrimaryWeaponSystem : BaseSpawnSystem, IEcsRunSystem
    {
        public SpawnPrimaryWeaponSystem(IGameObjectFactory factory) : base(factory) { }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<SpawnPrimaryWeaponRequest>().End();

            var requestPool = world.GetPool<SpawnPrimaryWeaponRequest>();
            var primaryWeaponPool = world.GetPool<PrimaryWeapon>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref requestPool.Get(entity).PrefabAddress;
                ref var weaponSlot = ref requestPool.Get(entity).WeaponSlot;

                SpawnPrimaryWeapon(entity, prefabAddress, weaponSlot, world, primaryWeaponPool);

                requestPool.Del(entity);
            }
        }

        private async void SpawnPrimaryWeapon(int ownerEntity, string prefabAddress, Transform weaponSlot, EcsWorld world, EcsPool<PrimaryWeapon> primaryWeaponPool)
        {
            var newEntityWithGameObject = await Spawn(new SpawnInfo
            {
                PrefabAddress = prefabAddress,
                Position = Vector2.zero,
                Rotation = Quaternion.identity,
                Parent = weaponSlot,
                World = world,
            });

            primaryWeaponPool.Add(ownerEntity).WeaponEntity = world.PackEntity(newEntityWithGameObject.Entity);
        }
    }
}