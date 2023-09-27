using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SecondaryWeapon
{
    public class SpawnSecondaryWeaponSystem : BaseSpawnSystem, IEcsRunSystem
    {
        public SpawnSecondaryWeaponSystem(IGameObjectFactory factory) : base(factory) { }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<SpawnSecondaryWeaponRequest>().End();

            var requestPool = world.GetPool<SpawnSecondaryWeaponRequest>();
            var weaponPool = world.GetPool<SecondaryWeapon>();

            foreach (var entity in filter)
            {
                ref var prefabAddress = ref requestPool.Get(entity).PrefabAddress;
                ref var weaponSlot = ref requestPool.Get(entity).WeaponSlot;

                SpawnPrimaryWeapon(entity, prefabAddress, weaponSlot, world, weaponPool);

                requestPool.Del(entity);
            }
        }

        private async void SpawnPrimaryWeapon(int ownerEntity, string prefabAddress, Transform weaponSlot, EcsWorld world, EcsPool<SecondaryWeapon> primaryWeaponPool)
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