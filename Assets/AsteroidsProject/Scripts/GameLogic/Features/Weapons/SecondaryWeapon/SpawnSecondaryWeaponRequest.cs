using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SecondaryWeapon
{
    [Serializable]
    public struct SpawnSecondaryWeaponRequest
    {
        public string PrefabAddress;
        public Transform WeaponSlot;
    }
}