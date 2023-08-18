using AsteroidsProject.Infrastructure.GameLogic;
using AsteroidsProject.Infrastructure.StateMachine;
using Zenject;

namespace AsteroidsProject.GameLogic
{
    public class GameOverState : IState
    {
        private readonly IGameStateMachine gameStateMachine;
        public GameOverState(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            //throw new System.NotImplementedException();
        }

        public void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameOverState>
        {
        }
    }
}
