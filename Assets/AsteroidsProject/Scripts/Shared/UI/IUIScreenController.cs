namespace AsteroidsProject.Shared
{
    public interface IUIScreenController
    {
        public bool IsVisible { get; }
        public void Show();
        public void Hide();
        public void DontDestroyOnLoad();
    }
}