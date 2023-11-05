using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnSecondaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnSecondaryWeaponRequest, CSecondaryWeapon>
    {
        public SpawnSecondaryWeaponSystem(IConfigProvider configProvider, IActiveGOMappingService activeGOMappingService)
            : base(configProvider, activeGOMappingService) { }

        protected override Transform GetWeaponSlot(EcsWorld world, int entity)
        {
            var goIDPool = world.GetPool<CGameObjectInstanceID>();
            ref var goID = ref goIDPool.Get(entity).Value;
            if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return default;
            return (goLink as IHaveSecondaryWeapon).SecondaryWeaponSlot;
        }
    }
}