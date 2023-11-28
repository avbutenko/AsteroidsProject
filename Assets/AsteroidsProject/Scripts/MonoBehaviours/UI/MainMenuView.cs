using AsteroidsProject.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidsProject.MonoBehaviours
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        public Button StartButton => startButton;
        public Button ExitButton => exitButton;
    }
}
