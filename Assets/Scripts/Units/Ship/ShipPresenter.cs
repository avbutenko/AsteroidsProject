using AsteroidsProject.Infrastructure;
using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Infrastructure.Views;
using System;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Units.Ship
{
    public class ShipPresenter : IShipPresenter, IInitializable, IDisposable, ITickable
    {
        public event Action<Quaternion> RotationChanged;
        public event Action<Vector3> PositionChanged;
        public event Action<float> ScaleChanged;

        private IShipModel model;
        private ISceneObjectView view;
        private IShipStateMachine stateMachine;
        private ITransformable initTransformParams;


        [Inject]
        public void Constract(
            IShipModel model,
            ISceneObjectView view,
            IShipStateMachine stateMachine,
            ITransformable initTransformParams)
        {
            this.model = model;
            this.view = view;
            this.stateMachine = stateMachine;
            this.initTransformParams = initTransformParams;
        }

        public void Initialize()
        {
            model.PositionChanged += Model_OnPositionChanged;
            model.RotationChanged += Model_OnRotationChanged;
            model.ScaleChanged += Model_ScaleChanged;
            view.OnCollision += View_OnCollision;

            model.Position = initTransformParams.Position;
            model.Rotation = initTransformParams.Rotation;
            model.Scale = initTransformParams.Scale;

            stateMachine.Enter<ShipIdleState>();
        }

        public void Tick()
        {
            stateMachine.Tick();
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

        public float Speed
        {
            get { return model.Speed; }
            set { model.Speed = value; }
        }

        private void View_OnCollision(Collision2D collision)
        {
            HandleCollision(collision);
        }

        private void HandleCollision(Collision2D collision)
        {
            //TODO
        }

        private void Model_OnPositionChanged(Vector3 vector)
        {
            PositionChanged?.Invoke(vector);
        }

        private void Model_OnRotationChanged(Quaternion quaternion)
        {
            RotationChanged?.Invoke(quaternion);
        }

        private void Model_ScaleChanged(float value)
        {
            ScaleChanged?.Invoke(value);
        }

        public void Dispose()
        {
            model.PositionChanged -= Model_OnPositionChanged;
            model.RotationChanged -= Model_OnRotationChanged;
            model.ScaleChanged -= Model_ScaleChanged;
            view.OnCollision -= View_OnCollision;
        }

        public class Factory : PlaceholderFactory<ITransformable, IShipPresenter>, IShipPresenterFactory
        {
        }
    }
}
