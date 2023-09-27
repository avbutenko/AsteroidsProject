using AsteroidsProject.Shared;
using System;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.SpawnWeapon
{
    [Serializable]
    public struct SpawnWeaponInfo
    {
        public string PrefabAddress;
        public Transform WeaponSlot;
        public WeaponType WeaponType;
    }
}