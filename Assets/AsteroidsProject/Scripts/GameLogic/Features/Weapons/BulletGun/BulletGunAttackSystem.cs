using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.BulletGun
{
    public class BulletGunAttackSystem : IEcsRunSystem
    {
        private readonly ISceneData sceneData;

        public BulletGunAttackSystem(ISceneData sceneData)
        {
            this.sceneData = sceneData;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<BulletGunTag>()
                              .Inc<AttackRequest>()
                              .Inc<ShootingPoint>()
                              .Inc<CoolDown>()
                              .Exc<ActiveCoolDown>()
                              .End();

            var shootingPointPool = world.GetPool<ShootingPoint>();
            var coolDownPool = world.GetPool<CoolDown>();
            var activeCoolDownPool = world.GetPool<ActiveCoolDown>();

            foreach (var entity in filter)
            {
                ref var shootingPoint = ref shootingPointPool.Get(entity).Value;
                ref var coolDown = ref coolDownPool.Get(entity).Value;

                world.NewEntityWith(new SpawnBulletRequest
                {
                    SpawnInfo = new SpawnInfo
                    {
                        Position = shootingPoint.position,
                        Rotation = shootingPoint.rotation,
                        Parent = sceneData.BulletsPool
                    }
                });

                activeCoolDownPool.Add(entity).Value = coolDown;
            }
        }
    }
}