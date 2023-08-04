using Assets.Scripts.Infrastructure.Services;
using AsteroidsProject.Infrastructure.Services;
using AsteroidsProject.Infrastructure.StateMachine;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Settings.Units.Ship;
using UnityEngine;

namespace AsteroidsProject.Units.Ship
{
    public abstract class ShipBaseMovingState : ITickableState
    {
        protected readonly IShipStateMachine stateMachine;
        protected readonly IShipInputService inputService;
        protected readonly IShipPresenter presenter;
        protected readonly ShipSettingsInstaller.ShipMovementSettings settings;
        protected readonly IRotationService rotationService;
        protected readonly ITeleportationService teleportationService;

        public ShipBaseMovingState(IShipStateMachine stateMachine,
            IShipInputService inputService,
            IShipPresenter presenter,
            ShipSettingsInstaller.ShipMovementSettings settings,
            IRotationService rotationService,
            ITeleportationService teleportationService)
        {
            this.stateMachine = stateMachine;
            this.inputService = inputService;
            this.presenter = presenter;
            this.settings = settings;
            this.rotationService = rotationService;
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
            Rotate();
            Move();
            CheckForTeleport();
        }

        protected virtual float GetCurrentSpeed()
        {
            return 0f;
        }

        private void Rotate()
        {
            presenter.Rotation = rotationService.GetNewRotation(presenter.Rotation, inputService.RotationDirection, settings.RotationSpeed);
        }

        private void Move()
        {
            Vector3 moveDirection = presenter.Rotation * Vector3.up;
            presenter.Speed = GetCurrentSpeed();
            presenter.Position += presenter.Speed * Time.deltaTime * moveDirection.normalized;
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