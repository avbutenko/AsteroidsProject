using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Spawn.Weapons
{
    public struct CSecondaryWeaponConfigAddress : IHaveConfigAddress
    {
        public string Config;

        public string ConfigAddress
        {
            get => Config;
            set => Config = value;
        }
    }
}