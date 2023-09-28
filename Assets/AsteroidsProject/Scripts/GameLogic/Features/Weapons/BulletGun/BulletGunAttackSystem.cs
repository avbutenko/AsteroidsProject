using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.BulletGun
{
    public class BulletGunAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private readonly ICoolDownService coolDownService;
        private BulletGunConfig config;

        public BulletGunAttackSystem(IConfigProvider configProvider, ICoolDownService coolDownService)
        {
            this.configProvider = configProvider;
            this.coolDownService = coolDownService;
        }
        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<BulletGunConfig>(gameConfig.BulletGunConfigPath);
            coolDownService.Value = config.Cooldown;
            coolDownService.Reset();
        }

        public void Run(IEcsSystems systems)
        {
            if (!coolDownService.IsReady) return;

            var world = systems.GetWorld();
            var filter = world.Filter<BulletGunTag>()
                              .Inc<AttackRequest>()
                              .Inc<ShootingPoint>()
                              .End();

            var shootingPointPool = world.GetPool<ShootingPoint>();

            foreach (var entity in filter)
            {
                ref var shootingPoint = ref shootingPointPool.Get(entity).Value;

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
            }

            coolDownService.Reset();
        }
    }
}