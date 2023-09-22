using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class PlayerGameObjectView : MonoBehaviour, IPlayerGameObject
    {
        [SerializeField] private Transform primaryWeaponSlot;
        [SerializeField] private Transform secondaryWeaponSlot;

        public Vector2 Position { set => transform.localPosition = value; }
        public Quaternion Rotation { set => transform.localRotation = value; }
        public Transform PrimaryWeaponSlot => primaryWeaponSlot;
        public Transform SecondaryWeaponSlot => secondaryWeaponSlot;
        public GameObject GameObject => gameObject;
    }
}