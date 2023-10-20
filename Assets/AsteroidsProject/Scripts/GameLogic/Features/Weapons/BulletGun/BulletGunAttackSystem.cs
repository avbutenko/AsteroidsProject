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
            var filter = world.Filter<CBulletGunTag>()
                              .Inc<CAttackRequest>()
                              .Inc<CCoolDown>()
                              .Exc<CActiveCoolDown>()
                              .End();

            var coolDownPool = world.GetPool<CCoolDown>();
            var activeCoolDownPool = world.GetPool<CActiveCoolDown>();
            var goLink = world.GetPool<CGameObject>();

            foreach (var entity in filter)
            {
                if (!goLink.Has(entity)) return;

                ref var view = ref goLink.Get(entity).Link;
                var shootingPoint = view as IHaveShootingPoint;
                ref var coolDown = ref coolDownPool.Get(entity).Value;

                world.NewEntityWith(new CSpawnBulletRequest
                {
                    SpawnInfo = new SpawnInfo
                    {
                        Position = shootingPoint.ShootingPoint.position,
                        Rotation = shootingPoint.ShootingPoint.rotation,
                        Parent = sceneData.BulletsPool
                    }
                });

                activeCoolDownPool.Add(entity).Value = coolDown;
            }
        }
    }
}