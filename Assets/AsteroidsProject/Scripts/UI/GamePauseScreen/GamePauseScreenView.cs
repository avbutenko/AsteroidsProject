using AsteroidsProject.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsProject.UI.GamePauseScreen
{
    public class GamePauseScreenView : MonoBehaviour, IGamePauseScreenView
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;

        public Button ResumeButton => resumeButton;
        public Button ExitButton => exitButton;

        public bool IsVisible => gameObject.activeSelf;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
