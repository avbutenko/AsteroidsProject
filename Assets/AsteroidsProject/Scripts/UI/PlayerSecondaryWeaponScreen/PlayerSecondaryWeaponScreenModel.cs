using AsteroidsProject.Shared;
using UniRx;

namespace AsteroidsProject.UI.PlayerSecondaryWeaponScreen
{
    public class PlayerSecondaryWeaponScreenModel : IPlayerSecondaryWeaponScreenModel
    {
        public IReactiveProperty<int> Ammo { get; private set; }
        public IReactiveProperty<float> AmmoAutoRefillCooldown { get; private set; }

        public PlayerSecondaryWeaponScreenModel()
        {
            Ammo = new ReactiveProperty<int>(0);
            AmmoAutoRefillCooldown = new ReactiveProperty<float>(0);
        }
    }
}