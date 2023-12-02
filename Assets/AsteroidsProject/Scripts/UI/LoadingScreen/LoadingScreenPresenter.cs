using AsteroidsProject.Shared;

namespace AsteroidsProject.UI.LoadingScreen
{
    public class LoadingScreenPresenter : ILoadingScreenPresenter
    {
        private readonly ILoadingScreenView view;

        public LoadingScreenPresenter(ILoadingScreenView view)
        {
            this.view = view;
        }

        public void DontDestroyOnLoad()
        {
            view.DontDestroyOnLoad();
        }

        public void Hide()
        {
            view.Hide();
        }

        public void Show()
        {
            view.Show();
        }
    }
}