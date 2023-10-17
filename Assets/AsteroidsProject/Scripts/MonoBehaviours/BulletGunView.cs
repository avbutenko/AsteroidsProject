using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class BulletGunView : GameObjectView, IHaveShootingPoint
    {
        [SerializeField] private Transform shootingPoint;
        public Transform ShootingPoint => shootingPoint;
    }
}