using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Weapons
{
    public class BulletGunAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CBulletGunTag>()
                          .Inc<CAttackRequest>()
                          .Exc<CAttackCoolDown>()
                          .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                world.NewEntityWith(new CAttackEvent { PackedEntity = world.PackEntity(entity) });
            }
        }
    }
}