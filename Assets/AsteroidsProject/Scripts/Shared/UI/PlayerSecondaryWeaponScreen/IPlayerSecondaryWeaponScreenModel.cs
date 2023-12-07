using UniRx;

namespace AsteroidsProject.Shared
{
    public interface IPlayerSecondaryWeaponScreenModel
    {
        public IReactiveProperty<int> Ammo { get; }
        public IReactiveProperty<float> AmmoAutoRefillCooldown { get; }
    }
}
