using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Spawn
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