using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using TMPro;
using UnityEngine.Scripting;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class GameOverScreenControlSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private readonly IUIProvider uiProvider;
        private readonly ISceneLoader sceneLoader;
        private IGameOverScreenController screenController;
        private IEcsSystems systems;

        [EcsUguiNamed(Identifiers.Ui.ScoreValueLabelName)]
        readonly TextMeshProUGUI score = default;

        public GameOverScreenControlSystem(IUIProvider uiProvider, ISceneLoader sceneLoader)
        {
            this.uiProvider = uiProvider;
            this.sceneLoader = sceneLoader;
        }

        public void Init(IEcsSystems systems)
        {
            screenController = uiProvider.Get<IGameOverScreenController>();
            this.systems = systems;
        }

        public override void Run(IEcsSystems systems)
        {
            base.Run(systems);

            if (screenController.IsVisible)
            {
                SetScore(systems);
            }
        }

        private void SetScore(IEcsSystems systems)
        {
            var world = systems.GetWorld(Identifiers.Worlds.GameWorldName);
            var filter = world.Filter<CScore>().End();
            var pool = world.GetPool<CScore>();

            foreach (var entity in filter)
            {
                var newValue = pool.Get(entity).Value.ToString();
                if (score.text != newValue)
                {
                    score.text = newValue;
                }
            }
        }

        [Preserve]
        [EcsUguiClickEvent(Identifiers.Ui.RestartButtonName)]
        private void OnClickRestartGame(in EcsUguiClickEvent e)
        {
            uiProvider.LoadingScreen.Show();
            screenController.Hide();
            var world = systems.GetWorld(Identifiers.Worlds.GameWorldName);
            world.NewEntityWith<CGameRestartEvent>();
            uiProvider.LoadingScreen.Hide();
        }

        [Preserve]
        [EcsUguiClickEvent(Identifiers.Ui.ExitButtonName)]
        private void OnClickExitGame(in EcsUguiClickEvent e)
        {
            HandleExit();
        }

        private async void HandleExit()
        {
            screenController.Hide();
            uiProvider.LoadingScreen.Show();
            await sceneLoader.LoadSceneAsync(Identifiers.Scenes.MainMenuSceneName);
            uiProvider.LoadingScreen.Hide();
        }
    }
}