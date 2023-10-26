using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Core
{
    public struct CSecondaryWeapon : IHaveLinkedEntity
    {
        public EcsPackedEntity WeaponEntity;

        public EcsPackedEntity LinkedEntity
        {
            get => WeaponEntity;
            set => WeaponEntity = value;
        }
    }
}