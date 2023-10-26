using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Core
{
    public struct CSpawnPrimaryWeaponRequest : IHaveConfigAddress
    {
        public string WeaponConfig;

        public string ConfigAddress
        {
            get => WeaponConfig;
            set => WeaponConfig = value;
        }
    }
}