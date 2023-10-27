using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Events.OnAttack
{
    public class OnAttackConverter : ComponentListConverter<COnAttack>
    {
        public OnAttackConverter(IComponentConverterService componentConverterService) : base(componentConverterService) { }
    }
}