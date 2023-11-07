using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Core
{
    public struct CSpawnSecondaryWeaponRequest : IHaveConfigAddress
    {
        public string Config;

        public string ConfigAddress
        {
            get => Config;
            set => Config = value;
        }
    }
}