using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.UI.LoadingScreen
{
    public class LoadingScreenFactory : IUIScreenFactory
    {
        private readonly IGameConfigProvider configProvider;

        public LoadingScreenFactory(IGameConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public IUIScreenPresenter Create()
        {
            var prefab = Resources.Load<GameObject>(configProvider.GameConfig.UIConfig.LoadingScreenPath);
            var presenter = Object.Instantiate(prefab);
            return presenter.GetComponent<IUIScreenPresenter>();
        }
    }
}