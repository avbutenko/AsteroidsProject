using AsteroidsProject.GameLogic;
using AsteroidsProject.Infrastructure.GameLogic;
using Zenject;

namespace AsteroidsProject.CompositionRoot.GameLogic
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IGameStateMachine, BootstrapState, BootstrapState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadLevelState, LoadLevelState.Factory>();
            Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();
            Container.BindFactory<IGameStateMachine, GamePauseState, GamePauseState.Factory>();
            Container.BindFactory<IGameStateMachine, GameOverState, GameOverState.Factory>();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }
    }
}

