using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Infrastructure.Views;
using System;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipPresenter : IShipPresenter, IInitializable, IDisposable, ITickable
    {
        private IShipModel model;
        private ISceneObjectView view;
        private IShipRotatingStateMachine rotatingStateMachine;
        private IShipMovingStateMachine movingStateMachine;
        private IShipInitParams shipInitParams;


        [Inject]
        public void Construct(
            IShipModel model,
            ISceneObjectView view,
            IShipRotatingStateMachine rotatingStateMachine,
            IShipMovingStateMachine movingStateMachine,
            IShipInitParams shipInitParams)
        {
            this.model = model;
            this.view = view;
            this.rotatingStateMachine = rotatingStateMachine;
            this.movingStateMachine = movingStateMachine;
            this.shipInitParams = shipInitParams;
        }

        public void Initialize()
        {
            model.PositionChanged += Model_OnPositionChanged;
            model.RotationChanged += Model_OnRotationChanged;
            model.ScaleChanged += Model_OnScaleChanged;

            model.Position = shipInitParams.Position;
            model.Rotation = shipInitParams.Rotation;
            model.Scale = shipInitParams.Scale;

            rotatingStateMachine.Enter<ShipNotRotatingState>();
            movingStateMachine.Enter<ShipNotMovingState>();
        }

        public void Tick()
        {
            rotatingStateMachine.Tick();
            movingStateMachine.Tick();
        }

        public Vector3 Position
        {
            get { return model.Position; }
            set { model.Position = value; }
        }
        public Quaternion Rotation
        {
            get { return model.Rotation; }
            set { model.Rotation = value; }
        }

        public float Scale
        {
            get { return model.Scale; }
            set { model.Scale = value; }
        }

        public float MovementSpeed
        {
            get { return model.MovementSpeed; }
            set { model.MovementSpeed = value; }
        }

        public float RatationSpeed
        {
            get { return model.MovementSpeed; }
            set { model.MovementSpeed = value; }
        }

        public Vector3 MovementDirection
        {
            get { return model.MovementDirection; }
            set { model.MovementDirection = value; }
        }

        public int RotationDirection { get; set; }

        private void Model_OnPositionChanged(Vector3 vector)
        {
            view.UpdatePosition(vector);
        }

        private void Model_OnRotationChanged(Quaternion quaternion)
        {
            view.UpdateRotation(quaternion);
        }

        private void Model_OnScaleChanged(float value)
        {
            view.UpdateScale(value);
        }

        public void Dispose()
        {
            model.PositionChanged -= Model_OnPositionChanged;
            model.RotationChanged -= Model_OnRotationChanged;
            model.ScaleChanged -= Model_OnScaleChanged;
        }

        public class Factory : PlaceholderFactory<IShipInitParams, IShipPresenter>, IShipPresenterFactory
        {
        }
    }
}
