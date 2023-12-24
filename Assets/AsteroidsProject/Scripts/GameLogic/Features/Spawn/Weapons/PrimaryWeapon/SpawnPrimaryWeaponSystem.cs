using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnPrimaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnPrimaryWeaponRequest, CPrimaryWeapon>
    {
        public SpawnPrimaryWeaponSystem(IConfigLoader configProvider, IActiveGOMappingService activeGOMappingService)
            : base(configProvider, activeGOMappingService) { }

        protected override Transform GetWeaponSlot(int entity)
        {
            ref var goID = ref goIDPool.Get(entity).Value;
            if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return default;
            return (goLink as IHavePrimaryWeapon).PrimaryWeaponSlot;
        }
    }
}