using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.Scripting;
using UnityEngine;
using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class MainMenuScreenControlSystem : EcsUguiCallbackSystem
    {
        private readonly IUIProvider uiProvider;
        private readonly ISceneLoader sceneLoader;

        public MainMenuScreenControlSystem(IUIProvider uiProvider, ISceneLoader sceneLoader)
        {
            this.uiProvider = uiProvider;
            this.sceneLoader = sceneLoader;
        }

        [Preserve]
        [EcsUguiClickEvent(Identifiers.Ui.StartButtonName)]
        void OnClickStartGame(in EcsUguiClickEvent e)
        {
            HandleOnClickStartGame();
        }

        [Preserve]
        [EcsUguiClickEvent(Identifiers.Ui.ExitButtonName)]
        void OnClickExitGame(in EcsUguiClickEvent e)
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private async void HandleOnClickStartGame()
        {
            uiProvider.LoadingScreen.Show();
            await sceneLoader.LoadSceneAsync(Identifiers.Scenes.GameSceneName);
            uiProvider.LoadingScreen.Hide();
        }
    }
}