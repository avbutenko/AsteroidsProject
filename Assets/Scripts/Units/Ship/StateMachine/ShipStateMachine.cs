using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipStateMachine : IShipStateMachine
    {
        private Dictionary<Type, ITickableState> registeredStates;
        private ITickableState currentState;

        [Inject]
        public void Construct(
            ShipIdleState.Factory shipIdleStateFactory,
            ShipIdleRotatingState.Factory shipIdleRotatingStateFactory,
            ShipAcceleratedMovingState.Factory shipAcceleratedMovingStateFactory,
            ShipInertMovingState.Factory shipInertMovingStateFactory)
        {
            registeredStates = new Dictionary<Type, ITickableState>();

            RegisterState(shipIdleStateFactory.Create(this));
            RegisterState(shipIdleRotatingStateFactory.Create(this));
            RegisterState(shipAcceleratedMovingStateFactory.Create(this));
            RegisterState(shipInertMovingStateFactory.Create(this));
        }

        protected void RegisterState<TState>(TState state) where TState : ITickableState =>
            registeredStates.Add(typeof(TState), state);

        public void Enter<TState>() where TState : class, ITickableState
        {
            TState newState = ChangeState<TState>();
            newState.Enter();
        }

        private TState ChangeState<TState>() where TState : class, ITickableState
        {
            currentState?.Exit();

            TState state = GetState<TState>();
            currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, ITickableState =>
            registeredStates[typeof(TState)] as TState;

        public void Tick()
        {
            Debug.Log(currentState);
            currentState.Tick();
        }
    }
}

