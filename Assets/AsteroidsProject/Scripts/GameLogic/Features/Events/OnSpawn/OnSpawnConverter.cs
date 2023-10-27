using AsteroidsProject.Shared;

namespace AsteroidsProject.GameLogic.Features.Events.OnSpawn
{
    public class OnSpawnConverter : ComponentListConverter<COnSpawn>
    {
        public OnSpawnConverter(IComponentConverterService componentConverterService) : base(componentConverterService) { }
    }
}