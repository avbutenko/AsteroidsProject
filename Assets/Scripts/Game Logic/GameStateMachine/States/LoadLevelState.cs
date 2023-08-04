using AsteroidsProject.Infrastructure.GameLogic;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using Zenject;

namespace AsteroidsProject.GameLogic
{
    public class LoadLevelState : IPaylodedState<string>
    {
        private readonly IGameStateMachine gameStateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly ILoadingCurtain loadingCurtain;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain)
        {
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            loadingCurtain.Show();
            sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            gameStateMachine.Enter<GameLoopState>();
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}
