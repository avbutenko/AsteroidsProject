using AsteroidsProject.Shared;
using UniRx;
using UnityEngine.SceneManagement;
using UniRx.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AsteroidsProject.UI.GameOverScreen
{
    public class GameOverScreenPresenter : MonoBehaviour, IGameOverScreenPresenter
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI score;

        private ISceneLoader sceneLoader;
        private ILoadingScreenService loadingScreen;
        private IRestartService restartService;
        private IGameOverScreenModel model;
        private CompositeDisposable trash;

        [Inject]
        public void Construct(IGameOverScreenModel model, ISceneLoader sceneLoader,
            ILoadingScreenService loadingScreen, IRestartService restartService)
        {
            this.model = model;
            this.sceneLoader = sceneLoader;
            this.loadingScreen = loadingScreen;
            this.restartService = restartService;
        }

        public void Awake()
        {
            trash = new CompositeDisposable();
            restartButton.OnClickAsObservable().Subscribe(_ => RestartButtonClick()).AddTo(trash);
            exitButton.OnClickAsObservable().Subscribe(_ => ExitButtonClick()).AddTo(trash);
            Hide();
        }

        public void Start()
        {
            model.Score.SubscribeToText(score).AddTo(trash);
        }

        public bool IsVisible => gameObject.activeSelf;

        public IReactiveProperty<int> Score
        {
            get => model.Score;
            set => model.Score.Value = value.Value;
        }

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

        private async void ExitButtonClick()
        {
            Hide();
            loadingScreen.Show();
            await sceneLoader.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single, false);
            loadingScreen.Hide();
        }

        private void RestartButtonClick()
        {
            Hide();
            restartService.Restart();
        }
    }
}