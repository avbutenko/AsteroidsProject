using Assets.AsteroidsProject.Scripts.Shared;

namespace AsteroidsProject.GameLogic.Core
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