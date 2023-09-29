using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.BulletGun
{
    public class BulletGunAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private BulletGunConfig config;

        public BulletGunAttackSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<BulletGunConfig>(gameConfig.BulletGunConfigPath);
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

                world.NewEntityWith(new SpawnPrefabRequest
                {
                    SpawnInfo = new SpawnInfo
                    {
                        PrefabAddress = config.ProjectilePrefabAddress,
                        Position = shootingPoint.position,
                        Rotation = shootingPoint.rotation,
                        Parent = null
                    }
                });

                activeCoolDownPool.Add(entity).Value = coolDown;
            }
        }
    }
}