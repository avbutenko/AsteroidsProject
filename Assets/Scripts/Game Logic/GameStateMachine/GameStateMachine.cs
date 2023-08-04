using AsteroidsProject.Infrastructure.GameLogic;
using AsteroidsProject.Infrastructure.StateMachine;
using System;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> registeredStates;
        private IExitableState currentState;

        public GameStateMachine(
            BootstrapState.Factory bootstrapStateFactory,
            LoadLevelState.Factory loadLevelStateFactory,
            GameLoopState.Factory gameLoopStateFactory,
            GamePauseState.Factory gamePauseStateFactory,
            GameOverState.Factory gameOverStateFactory)
        {
            registeredStates = new Dictionary<Type, IExitableState>();

            RegisterState(bootstrapStateFactory.Create(this));
            RegisterState(loadLevelStateFactory.Create(this));
            RegisterState(gameLoopStateFactory.Create(this));
            RegisterState(gamePauseStateFactory.Create(this));
            RegisterState(gameOverStateFactory.Create(this));
        }

        protected void RegisterState<TState>(TState state) where TState : IExitableState =>
            registeredStates.Add(typeof(TState), state);

        public void Enter<TState>() where TState : class, IState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            TState newState = ChangeState<TState>();
            newState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            currentState?.Exit();

            TState state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            registeredStates[typeof(TState)] as TState;
    }
}
