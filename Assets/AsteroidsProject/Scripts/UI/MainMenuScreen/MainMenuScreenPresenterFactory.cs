using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Zenject;

namespace AsteroidsProject.UI.MainMenuScreen
{
    public class MainMenuScreenPresenterFactory : IUIScreenPresenterFactoryAsync
    {
        private const string prefabAddress = "MainMenuScreen";
        private readonly IUIScreenViewFactory viewFactory;
        private readonly IInstantiator instantiator;

        public MainMenuScreenPresenterFactory(IInstantiator instantiator, IUIScreenViewFactory viewFactory)
        {
            this.instantiator = instantiator;
            this.viewFactory = viewFactory;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var view = await viewFactory.CreateAsync(prefabAddress);
            return instantiator.Instantiate<MainMenuScreenPresenter>(new object[] { view });
        }
    }
}