using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.Weapons
{
    public class LaserGunAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAmmo> ammoPool;
        private EcsPool<CAttackRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            filter = world.Filter<CLaserGunTag>()
                              .Inc<CAttackRequest>()
                              .Inc<CAmmo>()
                              .Exc<CAttackCoolDown>()
                              .End();

            ammoPool = world.GetPool<CAmmo>();
            requestPool = world.GetPool<CAttackRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                requestPool.Del(entity);
                ref var ammo = ref ammoPool.Get(entity).Value;

                if (ammo > 0)
                {
                    world.NewEntityWith(new CAttackEvent { PackedEntity = world.PackEntity(entity) });
                }
                else
                {
                    ammoPool.Del(entity);
                }
            }
        }
    }
}