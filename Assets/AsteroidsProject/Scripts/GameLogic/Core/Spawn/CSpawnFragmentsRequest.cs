using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Core
{
    public struct CSpawnFragmentsRequest : IHaveConfigAddress
    {
        public string Config;
        public int Amount;

        public string ConfigAddress
        {
            get => Config;
            set => Config = value;
        }
    }
}
