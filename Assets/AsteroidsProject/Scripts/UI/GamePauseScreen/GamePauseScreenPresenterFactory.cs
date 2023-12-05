using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Zenject;

namespace AsteroidsProject.UI.GamePauseScreen
{
    public class GamePauseScreenPresenterFactory : IUIScreenPresenterFactoryAsync
    {
        private const string prefabAddress = "GamePauseScreen";
        private readonly IUIScreenViewFactory viewFactory;
        private readonly IInstantiator instantiator;

        public GamePauseScreenPresenterFactory(IInstantiator instantiator, IUIScreenViewFactory viewFactory)
        {
            this.instantiator = instantiator;
            this.viewFactory = viewFactory;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var view = await viewFactory.CreateAsync(prefabAddress);
            var presenter = instantiator.Instantiate<GamePauseScreenPresenter>(new object[] { view });
            presenter.Initialize();
            return presenter;
        }
    }
}
