using AsteroidsProject.Infrastructure.Units.Ship;
using AsteroidsProject.Infrastructure.Views;
using System;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Views
{
    public class ShipView : MonoBehaviour, ISceneObjectView, IInitializable, IDisposable
    {
        public event Action<Collision2D> OnCollision;
        private IShipPresenter presenter;

        [Inject]
        public void Constract(IShipPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Initialize()
        {
            presenter.PositionChanged += Presenter_PositionChanged;
            presenter.RotationChanged += Presenter_RotationChanged;
            presenter.ScaleChanged += Presenter_ScaleChanged;
        }

        private void Presenter_PositionChanged(Vector3 vector)
        {
            transform.position = vector;
        }

        private void Presenter_RotationChanged(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }

        private void Presenter_ScaleChanged(float value)
        {
            transform.localScale = new Vector3(value, value, value);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke(collision);
        }

        public void Dispose()
        {
            presenter.PositionChanged -= Presenter_PositionChanged;
            presenter.RotationChanged -= Presenter_RotationChanged;
            presenter.ScaleChanged -= Presenter_ScaleChanged;
        }

        public class Factory : PlaceholderFactory<ShipView>
        {
        }
    }
}
