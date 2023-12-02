using UnityEngine.UI;

namespace AsteroidsProject.Shared
{
    public interface IMainMenuScreenView : IUIScreenView
    {
        public Button StartButton { get; }
        public Button ExitButton { get; }
    }
}