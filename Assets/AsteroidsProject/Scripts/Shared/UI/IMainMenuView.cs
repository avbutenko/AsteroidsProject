using UnityEngine.UI;

namespace AsteroidsProject.Shared
{
    public interface IMainMenuView
    {
        public Button StartButton { get; }
        public Button ExitButton { get; }
    }
}