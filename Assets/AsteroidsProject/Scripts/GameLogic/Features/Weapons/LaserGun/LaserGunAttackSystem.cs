using AsteroidsProject.Configs;
using AsteroidsProject.GameLogic.Core;
using AsteroidsProject.Shared;
using Leopotam.EcsLite;

namespace AsteroidsProject.GameLogic.Features.LaserGun
{
    public class LaserGunAttackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly IConfigProvider configProvider;
        private LaserGunConfig config;

        public LaserGunAttackSystem(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public async void Init(IEcsSystems systems)
        {
            var gameConfig = await configProvider.Load<GameConfig>("Configs/GameConfig.json");
            config = await configProvider.Load<LaserGunConfig>(gameConfig.LaserGunConfigPath);
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<LaserGunTag>()
                              .Inc<AttackRequest>()
                              .Inc<ShootingPoint>()
                              .Inc<CoolDown>()
                              .Inc<Ammo>()
                              .Inc<LinkToGameObject>()
                              .Exc<ActiveCoolDown>()
                              .End();

            var shootingPointPool = world.GetPool<ShootingPoint>();
            var coolDownPool = world.GetPool<CoolDown>();
            var activeCoolDownPool = world.GetPool<ActiveCoolDown>();
            var ammoPool = world.GetPool<Ammo>();
            var viewPool = world.GetPool<LinkToGameObject>();

            foreach (var entity in filter)
            {
                ref var shootingPoint = ref shootingPointPool.Get(entity).Value;
                ref var coolDown = ref coolDownPool.Get(entity).Value;
                ref var ammo = ref ammoPool.Get(entity).Value;
                ref var view = ref viewPool.Get(entity).View;

                if (ammo > 0)
                {
                    world.NewEntityWith(new SpawnPrefabRequest
                    {
                        SpawnInfo = new SpawnInfo
                        {
                            PrefabAddress = config.ProjectilePrefabAddress,
                            Parent = shootingPoint.transform
                        }
                    });

                    activeCoolDownPool.Add(entity).Value = coolDown;
                    ammo--;
                }
                else
                {
                    ammoPool.Del(entity);
                }
            }
        }
    }
}