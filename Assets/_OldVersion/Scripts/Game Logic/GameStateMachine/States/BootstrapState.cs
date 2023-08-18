using AsteroidsProject.Infrastructure.GameLogic;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using Zenject;

namespace AsteroidsProject.GameLogic
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            InitServices();
            gameStateMachine.Enter<LoadLevelState, string>(InfrastructureAssetPath.StartGameScene);
        }

        private void InitServices()
        {
            //Extend when required
        }

        public void Exit()
        {

        }

        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
        {
        }
    }
}
