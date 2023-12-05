using UnityEngine.UI;

namespace AsteroidsProject.Shared
{
    public interface IGamePauseScreenView : IUIScreenView
    {
        public Button ResumeButton { get; }
        public Button ExitButton { get; }
    }
}