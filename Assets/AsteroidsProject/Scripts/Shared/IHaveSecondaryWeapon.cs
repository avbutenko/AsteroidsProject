using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IHaveSecondaryWeapon
    {
        public Transform SecondaryWeaponSlot { get; }
    }
}