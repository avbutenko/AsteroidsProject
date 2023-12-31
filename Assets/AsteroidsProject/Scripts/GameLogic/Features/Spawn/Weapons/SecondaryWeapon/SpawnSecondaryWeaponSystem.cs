﻿using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnSecondaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnSecondaryWeaponRequest, CSecondaryWeapon>
    {
        public SpawnSecondaryWeaponSystem(IConfigLoader configProvider, IActiveGOMappingService activeGOMappingService)
            : base(configProvider, activeGOMappingService) { }

        protected override Transform GetWeaponSlot(int entity)
        {
            ref var goID = ref goIDPool.Get(entity).Value;
            if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return default;
            return (goLink as IHaveSecondaryWeapon).SecondaryWeaponSlot;
        }
    }
}