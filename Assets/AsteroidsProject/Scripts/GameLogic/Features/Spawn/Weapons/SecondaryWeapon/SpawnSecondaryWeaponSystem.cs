using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnSecondaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnSecondaryWeaponRequest, CSecondaryWeapon, CSecondaryWeaponConfigAddress>
    {
        public SpawnSecondaryWeaponSystem(IConfigProvider configProvider) : base(configProvider) { }

        protected override Transform GetWeaponSlot(EcsWorld world, int entity)
        {
            var linkToGameObjectPool = world.GetPool<CGameObject>();
            ref var view = ref linkToGameObjectPool.Get(entity).Link;
            return (view as IHaveSecondaryWeapon).SecondaryWeaponSlot;
        }
    }
}