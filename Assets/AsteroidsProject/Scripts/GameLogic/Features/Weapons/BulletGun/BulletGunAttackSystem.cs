using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;
using UnityEngine;

namespace AsteroidsProject.GameLogic.Features.Weapons
{
    public class BulletGunAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter filter;
        private EcsPool<CAttackRequest> requestPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            requestPool = world.GetPool<CAttackRequest>();
            filter = world.Filter<CBulletGunTag>()
                          .Inc<CAttackRequest>()
                          .Exc<CAttackCoolDown>()
                          .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter)
            {
                requestPool.Del(entity);
                world.NewEntityWith(new CAttackEvent { PackedEntity = world.PackEntity(entity) });
            }
        }
    }
}