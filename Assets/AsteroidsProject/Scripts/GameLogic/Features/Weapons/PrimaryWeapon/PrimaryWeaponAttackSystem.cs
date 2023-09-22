using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.PrimaryWeapon
{
    public class PrimaryWeaponAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var requestFilter = world.Filter<PrimaryWeaponAttackRequest>().End();
            var weaponFilter = world.Filter<PrimaryWeaponTag>().End();

            foreach (var requestEntity in requestFilter)
            {
                foreach (var weaponEntity in weaponFilter)
                {
                    world.AddComponentToEntity(weaponEntity, new AttackRequest { });
                }

                world.DelEntity(requestEntity);
            }
        }
    }
}