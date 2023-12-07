using AsteroidsProject.Shared;
using UniRx;
using UnityEngine;

namespace AsteroidsProject.UI.PlayerShipStatsScreen
{
    public class PlayerShipStatsScreenModel : IPlayerShipStatsScreenModel
    {
        public IReactiveProperty<int> Health { get; private set; }

        public IReactiveProperty<Vector2> Position { get; private set; }

        public IReactiveProperty<float> Rotation { get; private set; }

        public IReactiveProperty<float> Velocity { get; private set; }

        public PlayerShipStatsScreenModel()
        {
            Health = new ReactiveProperty<int>(0);
            Position = new ReactiveProperty<Vector2>(Vector2.zero);
            Rotation = new ReactiveProperty<float>(0);
            Velocity = new ReactiveProperty<float>(0);
        }
    }
}