using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public struct CSpawnPrimaryWeaponRequest : IHaveConfigAddress
    {
        public string Config;

        public string ConfigAddress
        {
            get => Config;
            set => Config = value;
        }
    }
}