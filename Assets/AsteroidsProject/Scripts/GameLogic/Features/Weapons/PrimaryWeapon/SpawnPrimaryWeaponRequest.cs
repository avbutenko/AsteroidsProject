using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.PrimaryWeapon
{
    [Serializable]
    public struct SpawnPrimaryWeaponRequest
    {
        public string PrefabAddress;
        public Transform WeaponSlot;
    }
}