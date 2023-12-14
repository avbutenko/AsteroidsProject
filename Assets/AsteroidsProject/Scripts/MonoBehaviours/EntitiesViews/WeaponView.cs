using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class WeaponView : GameObjectView, IHaveShootingPoint
    {
        [SerializeField] private Transform shootingPoint;
        public Transform ShootingPoint => shootingPoint;
    }
}