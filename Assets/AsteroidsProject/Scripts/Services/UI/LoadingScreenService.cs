using AsteroidsProject.Shared;
using Zenject;

namespace AsteroidsProject.Services
{
    public class LoadingScreenService : ILoadingScreenService, IInitializable
    {
        private readonly IUIScreenPresenterFactory factory;
        private ILoadingScreenPresenter presenter;

        public LoadingScreenService(IUIScreenPresenterFactory factory)
        {
            this.factory = factory;
        }

        public void Initialize()
        {
            presenter = (ILoadingScreenPresenter)factory.Create();
            presenter.DontDestroyOnLoad();
        }

        public bool IsVisible => presenter.IsVisible;

        public void Show()
        {
            presenter.Show();
        }

        public void Hide()
        {
            presenter.Hide();
        }
    }
}