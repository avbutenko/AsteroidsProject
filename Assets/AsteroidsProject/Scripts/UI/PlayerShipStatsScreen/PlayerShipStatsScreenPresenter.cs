using AsteroidsProject.Shared;
using TMPro;
using UniRx;
using UnityEngine;
using UniRx.Extensions;
using Zenject;
using System;
using UnityEngine.UIElements;

namespace AsteroidsProject.UI.PlayerShipStatsScreen
{
    public class PlayerShipStatsScreenPresenter : MonoBehaviour, IPlayerShipStatsScreenPresenter
    {
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI position;
        [SerializeField] private TextMeshProUGUI rotation;
        [SerializeField] private TextMeshProUGUI velocity;

        private IPlayerShipStatsScreenModel model;
        private CompositeDisposable trash;

        [Inject]
        public void Construct(IPlayerShipStatsScreenModel model)
        {
            this.model = model;
        }

        public void Awake()
        {
            trash = new CompositeDisposable();
            Hide();
        }

        public void Start()
        {
            model.Health.DistinctUntilChanged().SubscribeToText(health).AddTo(trash);
            model.Position.DistinctUntilChanged().SubscribeToText(position).AddTo(trash);
            model.Rotation.DistinctUntilChanged().SubscribeToText(rotation).AddTo(trash);
            model.Velocity.DistinctUntilChanged().SubscribeToText(velocity).AddTo(trash);
        }

        public int Health
        {
            get => model.Health.Value;
            set => model.Health.Value = value;
        }
        public Vector2 Position
        {
            get => model.Position.Value;
            set => model.Position.Value = value;
        }
        public float Rotation
        {
            get => model.Rotation.Value;
            set => model.Rotation.Value = (float)Math.Round(value, 2);
        }
        public float Velocity
        {
            get => model.Velocity.Value;
            set => model.Velocity.Value = (float)Math.Round(value, 2);
        }

        public bool IsVisible => gameObject.activeSelf;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void OnDestroy()
        {
            trash.Dispose();
        }
    }
}