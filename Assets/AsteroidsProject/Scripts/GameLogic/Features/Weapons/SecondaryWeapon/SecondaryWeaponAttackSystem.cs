using AsteroidsProject.GameLogic.Core;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.SecondaryWeapon
{
    public class SecondaryWeaponAttackSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var requestFilter = world.Filter<SecondaryWeaponAttackRequest>().End();
            var weaponFilter = world.Filter<SecondaryWeaponTag>().End();

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