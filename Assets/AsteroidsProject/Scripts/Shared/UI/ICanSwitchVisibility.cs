namespace AsteroidsProject.Shared
{
    public interface ICanSwitchVisibility
    {
        public bool IsVisible { get; }
        public void Show();
        public void Hide();
    }
}