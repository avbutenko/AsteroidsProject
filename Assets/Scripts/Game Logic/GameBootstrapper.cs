using AsteroidsProject.Infrastructure.GameLogic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.GameLogic
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        public class Factory : PlaceholderFactory<GameBootstrapper>
        {
        }
    }
}