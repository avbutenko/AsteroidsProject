using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Spawn.Projectiles
{
    public struct CProjectileConfig : IHaveConfigAddress
    {
        public string Config;

        public string ConfigAddress
        {
            get => Config;
            set => Config = value;
        }
    }
}