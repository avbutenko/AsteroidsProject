using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using TMPro;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class PlayerSecondaryWeaponScreenControlSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private readonly IUIProvider uiProvider;
        private IPlayerShipSecondaryWeaponScreenController screenController;

        [EcsUguiNamed(Identifiers.Ui.AmmoLabelName)]
        readonly TextMeshProUGUI ammo = default;

        [EcsUguiNamed(Identifiers.Ui.RefillCoolDownLabelName)]
        readonly TextMeshProUGUI refillCooldown = default;

        public PlayerSecondaryWeaponScreenControlSystem(IUIProvider uiProvider)
        {
            this.uiProvider = uiProvider;
        }

        public void Init(IEcsSystems systems)
        {
            screenController = uiProvider.Get<IPlayerShipSecondaryWeaponScreenController>();
        }

        public override void Run(IEcsSystems systems)
        {
            base.Run(systems);

            if (screenController.IsVisible)
            {
                SetValues(systems);
            }
        }

        private void SetValues(IEcsSystems systems)
        {
            var world = systems.GetWorld(Identifiers.Worlds.GameWorldName);
            var filter = world.Filter<CPlayerTag>().Inc<CSecondaryWeapon>().End();
            var weaponPool = world.GetPool<CSecondaryWeapon>();
            var ammoPool = world.GetPool<CAmmo>();
            var cooldownPool = world.GetPool<CAmmoAutoRefillCoolDown>();

            foreach (var entity in filter)
            {
                ref var weaponPackedEntity = ref weaponPool.Get(entity).WeaponEntity;

                if (weaponPackedEntity.Unpack(world, out var weaponEntity))
                {
                    if (ammoPool.Has(weaponEntity))
                    {
                        ammo.text = ammoPool.Get(weaponEntity).Value.ToString();
                    }
                    else
                    {
                        ammo.text = "0";
                    }

                    if (cooldownPool.Has(weaponEntity))
                    {
                        refillCooldown.text = cooldownPool.Get(weaponEntity).Value.ToString();
                    }
                    else
                    {
                        refillCooldown.text = "0";
                    }
                }
            }
        }
    }
}