using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Events.OnOutOfLevel
{
    public class OnOutOfLevelConverter : ComponentListConverter<COnOutOfLevel>
    {
        public OnOutOfLevelConverter(IComponentConverterService componentConverterService) : base(componentConverterService) { }
    }
}