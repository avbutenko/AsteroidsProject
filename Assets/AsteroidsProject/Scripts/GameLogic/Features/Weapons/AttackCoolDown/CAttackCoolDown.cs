using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Weapons
{
    public struct CAttackCoolDown : IHaveTimer
    {
        public float Value;

        public float Timer
        {
            get => Value;
            set => Value = value;
        }
    }
}