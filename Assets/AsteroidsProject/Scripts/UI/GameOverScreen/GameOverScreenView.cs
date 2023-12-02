using AsteroidsProject.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsProject.MonoBehaviours
{
    public class GameOverScreenView : MonoBehaviour, IGameOverScreenView
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        public Button RestartButton => restartButton;
        public Button ExitButton => exitButton;

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