using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IPlayerGameObject : IGameObject
    {
        public Transform PrimaryWeaponSlot { get; }
        public Transform SecondaryWeaponSlot { get; }
    }
}