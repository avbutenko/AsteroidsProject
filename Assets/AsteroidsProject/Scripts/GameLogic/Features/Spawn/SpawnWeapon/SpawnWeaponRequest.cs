using System;

namespace AsteroidsProject.GameLogic.Features.SpawnWeapon
{
    [Serializable]
    public struct SpawnWeaponRequest
    {
        public SpawnWeaponInfo[] Info;
    }
}