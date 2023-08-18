using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using UnityEngine;

namespace AsteroidsProject.Units.Ship
{
    public abstract class ShipBaseMovingState : ITickableState
    {
        protected readonly ITickableStateMachine stateMachine;
        protected readonly IShipInputService inputService;
        protected readonly IShipPresenter presenter;
        protected readonly ITeleportationService teleportationService;

        public ShipBaseMovingState(
            ITickableStateMachine stateMachine,
            IShipInputService inputService,
            IShipPresenter presenter,
            ITeleportationService teleportationService)
        {
            this.stateMachine = stateMachine;
            this.inputService = inputService;
            this.presenter = presenter;
            this.teleportationService = teleportationService;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Tick()
        {
            Move();
            CheckForTeleport();
        }

        protected abstract float GetCurrentSpeed();

        private void Move()
        {
            presenter.MovementSpeed = GetCurrentSpeed();
            //presenter.Position += presenter.MovementSpeed * Time.deltaTime * presenter.MovementDirection.normalized;
            var newPosition = presenter.Position + presenter.MovementSpeed * Time.deltaTime * presenter.MovementDirection.normalized;
            presenter.Position = Vector3.Lerp(presenter.Position, newPosition, presenter.MovementSpeed * Time.deltaTime);
        }

        private void CheckForTeleport()
        {
            if (teleportationService.IsOutOfLevel(presenter.Position, presenter.Scale))
            {
                presenter.Position = teleportationService.Teleport(presenter.Position, presenter.Scale);
            }
        }
    }
}