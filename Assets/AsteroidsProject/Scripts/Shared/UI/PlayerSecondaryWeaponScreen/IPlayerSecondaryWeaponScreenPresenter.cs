namespace AsteroidsProject.Shared
{
    public interface IPlayerSecondaryWeaponScreenPresenter : IUIScreenPresenter
    {
        public int Ammo { get; set; }
        public float AmmoAutoRefillCooldown { get; set; }
    }
}