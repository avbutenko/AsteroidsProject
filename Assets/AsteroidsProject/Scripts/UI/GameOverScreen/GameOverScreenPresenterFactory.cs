using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using Zenject;

namespace AsteroidsProject.UI.GameOverScreen
{
    public class GameOverScreenPresenterFactory : IUIScreenPresenterFactoryAsync
    {
        private const string prefabAddress = "GameOverScreen";
        private readonly IUIScreenViewFactory viewFactory;
        private readonly IInstantiator instantiator;

        public GameOverScreenPresenterFactory(IInstantiator instantiator, IUIScreenViewFactory viewFactory)
        {
            this.instantiator = instantiator;
            this.viewFactory = viewFactory;
        }

        public async UniTask<IUIScreenPresenter> CreateAsync()
        {
            var view = await viewFactory.CreateAsync(prefabAddress);
            return instantiator.Instantiate<GameOverScreenPresenter>(new object[] { view });
        }
    }
}