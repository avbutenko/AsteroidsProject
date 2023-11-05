using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnPrimaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnPrimaryWeaponRequest, CPrimaryWeapon>
    {
        public SpawnPrimaryWeaponSystem(IConfigProvider configProvider, IActiveGOMappingService activeGOMappingService)
            : base(configProvider, activeGOMappingService) { }

        protected override Transform GetWeaponSlot(EcsWorld world, int entity)
        {
            var goIDPool = world.GetPool<CGameObjectInstanceID>();
            ref var goID = ref goIDPool.Get(entity).Value;
            if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return default;
            return (goLink as IHavePrimaryWeapon).PrimaryWeaponSlot;
        }
    }
}