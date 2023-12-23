using AsteroidsProject.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace AsteroidsProject.UI.GamePauseScreen
{
    public class GamePauseScreenPresenter : MonoBehaviour, IGamePauseScreenPresenter
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;

        private ITimeService timeService;
        private ISceneLoader sceneLoader;
        //private ILoadingScreenService loadingScreen;
        private CompositeDisposable trash;

        [Inject]
        public void Construct(ITimeService timeService, ISceneLoader sceneLoader) //, ILoadingScreenService loadingScreen)
        {
            this.timeService = timeService;
            this.sceneLoader = sceneLoader;
            //this.loadingScreen = loadingScreen;
        }

        public void Awake()
        {
            trash = new CompositeDisposable();
            resumeButton.OnClickAsObservable().Subscribe(_ => ResumeButtonClick()).AddTo(trash);
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
            timeService.TooglePause();
        }

        public void OnDestroy()
        {
            trash.Dispose();
        }

        private void ResumeButtonClick()
        {
            timeService.TooglePause();
            Hide();
        }

        private async void ExitButtonClick()
        {
            Hide();
            timeService.TooglePause();
            //loadingScreen.Show();
            await sceneLoader.LoadSceneAsync(AssetLabels.MainMenuScene.ToString(), LoadSceneMode.Single, false);
            //loadingScreen.Hide();
        }
    }
}