using AsteroidsProject.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace AsteroidsProject.UI.MainMenuScreen
{
    public class MainMenuScreenPresenter : MonoBehaviour, IMainMenuScreenPresenter
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        private ISceneLoader sceneLoader;
        private ILoadingScreenService loadingScreen;
        private CompositeDisposable trash;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, ILoadingScreenService loadingScreen)
        {
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
        }

        public void Awake()
        {
            trash = new CompositeDisposable();
            startButton.OnClickAsObservable().Subscribe(_ => StartButtonClick()).AddTo(trash);
            exitButton.OnClickAsObservable().Subscribe(_ => ExitButtonClick()).AddTo(trash);
            Hide();
        }

        public bool IsVisible => gameObject.activeSelf;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void OnDestroy()
        {
            trash.Dispose();
        }

        private async void StartButtonClick()
        {
            loadingScreen.Show();
            await sceneLoader.LoadSceneAsync("GameScene", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }

        private void ExitButtonClick()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}