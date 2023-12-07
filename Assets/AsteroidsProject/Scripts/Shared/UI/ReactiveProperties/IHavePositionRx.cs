using UniRx;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IHavePositionRx
    {
        public IReactiveProperty<Vector2> Position { get; }
    }
}