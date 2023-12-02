using UnityEngine.UI;

namespace AsteroidsProject.Shared
{
    public interface IGameOverScreenView : IUIScreenView
    {
        public Button RestartButton { get; }
        public Button ExitButton { get; }
    }
}