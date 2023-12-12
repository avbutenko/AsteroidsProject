using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using System;

namespace AsteroidsProject.GameLogic.Features.UI.BroadcastDataToPlayerSecondaryWeaponScreen
{
    public class BroadcastDataToPlayerSecondaryWeaponScreenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IUIService uiService;
        private IPlayerSecondaryWeaponScreenPresenter screenPresenter;
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CSecondaryWeapon> weaponPool;
        private EcsPool<CAmmo> ammoPool;
        private EcsPool<CAmmoAutoRefillCoolDown> cooldownPool;

        public BroadcastDataToPlayerSecondaryWeaponScreenSystem(IUIService uiService)
        {
            this.uiService = uiService;
        }

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CPlayerTag>()
                          .Inc<CSecondaryWeapon>()
                          .End();

            weaponPool = world.GetPool<CSecondaryWeapon>();
            ammoPool = world.GetPool<CAmmo>();
            cooldownPool = world.GetPool<CAmmoAutoRefillCoolDown>();
            screenPresenter = uiService.Get<IPlayerSecondaryWeaponScreenPresenter>();
            screenPresenter.Show();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                ref var weaponPackedEntity = ref weaponPool.Get(entity).WeaponEntity;

                if (weaponPackedEntity.Unpack(world, out var weaponEntity))
                {
                    if (ammoPool.Has(weaponEntity))
                    {
                        ref var ammo = ref ammoPool.Get(weaponEntity).Value;
                        screenPresenter.Ammo = ammo;
                    }
                    else
                    {
                        screenPresenter.Ammo = 0;
                    }

                    if (cooldownPool.Has(weaponEntity))
                    {
                        ref var cooldown = ref cooldownPool.Get(weaponEntity).Value;
                        screenPresenter.AmmoAutoRefillCooldown = cooldown;
                    }
                    else
                    {
                        screenPresenter.AmmoAutoRefillCooldown = 0;
                    }
                }
            }
        }
    }
}