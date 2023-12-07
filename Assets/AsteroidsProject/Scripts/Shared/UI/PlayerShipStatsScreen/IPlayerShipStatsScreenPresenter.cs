using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IPlayerShipStatsScreenPresenter : IUIScreenPresenter
    {
        public int Health { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Velocity { get; set; }
    }
}