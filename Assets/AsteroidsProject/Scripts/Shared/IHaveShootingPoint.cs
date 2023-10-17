using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IHaveShootingPoint
    {
        public Transform ShootingPoint { get; }
    }
}