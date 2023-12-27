using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class InitMainMenuSceneUISystem : IEcsInitSystem
    {
        private readonly IUIProvider uiProvider;

        public InitMainMenuSceneUISystem(IUIProvider uiProvider)
        {
            this.uiProvider = uiProvider;
        }

        public void Init(IEcsSystems systems)
        {
            uiProvider.Get<IMainMenuScreenController>().Show();
        }
    }
}