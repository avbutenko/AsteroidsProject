using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public class SpawnSecondaryWeaponSystem : BaseSpawnWeaponSystem<CSpawnSecondaryWeaponRequest, CSecondaryWeapon>
    {
        private readonly IUIService uiService;
        public SpawnSecondaryWeaponSystem(IConfigProvider configProvider, IActiveGOMappingService activeGOMappingService, IUIService uiService)
            : base(configProvider, activeGOMappingService)
        {
            this.uiService = uiService;
        }

        public override void Init(IEcsSystems systems)
        {
            base.Init(systems);
            uiService.Get<IPlayerSecondaryWeaponScreenPresenter>().Show();
        }

        protected override Transform GetWeaponSlot(int entity)
        {
            ref var goID = ref goIDPool.Get(entity).Value;
            if (!activeGOMappingService.TryGetGoLink(goID, out var goLink)) return default;
            return (goLink as IHaveSecondaryWeapon).SecondaryWeaponSlot;
        }
    }
}