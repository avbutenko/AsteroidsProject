using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.Infrastructure.StateMachine
{
    public abstract class BaseTickableStateMachine : ITickableStateMachine
    {
        private Dictionary<Type, ITickableState> registeredStates;
        private ITickableState currentState;

        [Inject]
        public void Construct()
        {
            registeredStates = new Dictionary<Type, ITickableState>();
        }

        public void Enter<TState>() where TState : class, ITickableState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Tick()
        {
            currentState.Tick();
        }

        protected void RegisterState<TState>(TState state) where TState : ITickableState =>
            registeredStates.Add(typeof(TState), state);

        private TState ChangeState<TState>() where TState : class, ITickableState
        {
            currentState?.Exit();

            TState state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, ITickableState =>
            registeredStates[typeof(TState)] as TState;
    }
}

