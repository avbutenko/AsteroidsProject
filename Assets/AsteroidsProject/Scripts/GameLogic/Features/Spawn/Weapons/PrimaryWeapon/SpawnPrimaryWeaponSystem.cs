using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnPrimaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnPrimaryWeaponRequest, CPrimaryWeapon, CPrimaryWeaponConfigAddress>
    {
        public SpawnPrimaryWeaponSystem(IConfigProvider configProvider) : base(configProvider) { }

        protected override Transform GetWeaponSlot(EcsWorld world, int entity)
        {
            var linkToGameObjectPool = world.GetPool<CGameObject>();
            ref var view = ref linkToGameObjectPool.Get(entity).Link;
            return (view as IHavePrimaryWeapon).PrimaryWeaponSlot;
        }
    }
}