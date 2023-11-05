using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Core
{
    public struct CSpawnProjectileRequest : IHaveConfigAddress
    {
        public string Config;

        public string ConfigAddress
        {
            readonly get => Config;
            set => Config = value;
        }
    }
}