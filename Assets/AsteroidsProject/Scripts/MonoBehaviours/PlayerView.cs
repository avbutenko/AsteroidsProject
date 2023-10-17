using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class PlayerView : GameObjectView, IPlayerView
    {
        [SerializeField] private Transform primaryWeaponSlot;
        [SerializeField] private Transform secondaryWeaponSlot;

        public Transform PrimaryWeaponSlot => primaryWeaponSlot;
        public Transform SecondaryWeaponSlot => secondaryWeaponSlot;
    }
}