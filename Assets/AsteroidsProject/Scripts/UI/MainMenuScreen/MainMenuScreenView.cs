using AsteroidsProject.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsProject.MonoBehaviours
{
    public class MainMenuScreenView : MonoBehaviour, IMainMenuScreenView
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        public Button StartButton => startButton;
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