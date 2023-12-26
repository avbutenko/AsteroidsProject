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
            var world = systems.GetWorld(Identifiers.Worlds.GameWorldName);
            var filter = world.Filter<CGameOverEvent>().End();
            var pool = world.GetPool<CGameOverEvent>();

            foreach (var entity in filter)
            {
                screenController.Show();
                var scoreFilter = world.Filter<CScore>().End();
                var scrorePool = world.GetPool<CScore>();

                foreach (var scoreEntity in scoreFilter)
                {
                    score.text = scrorePool.Get(scoreEntity).Value.ToString();
                }

                pool.Del(entity);
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