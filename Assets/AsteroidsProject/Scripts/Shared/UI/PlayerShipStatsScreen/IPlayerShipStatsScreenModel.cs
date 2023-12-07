using UniRx;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IPlayerShipStatsScreenModel
    {
        public IReactiveProperty<int> Health { get; }
        public IReactiveProperty<Vector2> Position { get; }
        public IReactiveProperty<float> Rotation { get; }
        public IReactiveProperty<float> Velocity { get; }
    }
}