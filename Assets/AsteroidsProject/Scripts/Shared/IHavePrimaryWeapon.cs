using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IHavePrimaryWeapon
    {
        public Transform PrimaryWeaponSlot { get; }
    }
}