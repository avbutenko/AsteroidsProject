using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite;
using AsteroidsProject.Shared;
using TMPro;
using AsteroidsProject.GameLogic.Core;

namespace AsteroidsProject.GameLogic.Features.UI
{
    public class PlayerShipStatsScreenControlSystem : EcsUguiCallbackSystem, IEcsInitSystem
    {
        private readonly IUIProvider uiProvider;
        private IPlayerShipStatsScreenController screenController;

        [EcsUguiNamed(Identifiers.Ui.HealthLabelName)]
        readonly TextMeshProUGUI health = default;

        [EcsUguiNamed(Identifiers.Ui.PositionLabelName)]
        readonly TextMeshProUGUI position = default;

        [EcsUguiNamed(Identifiers.Ui.RotationLabelName)]
        readonly TextMeshProUGUI rotation = default;

        [EcsUguiNamed(Identifiers.Ui.VelocityLabelName)]
        readonly TextMeshProUGUI velocity = default;

        public PlayerShipStatsScreenControlSystem(IUIProvider uiProvider)
        {
            this.uiProvider = uiProvider;
        }

        public void Init(IEcsSystems systems)
        {
            screenController = uiProvider.Get<IPlayerShipStatsScreenController>();
        }

        public override void Run(IEcsSystems systems)
        {
            base.Run(systems);

            if (screenController.IsVisible)
            {
                SetValues(systems);
            }
        }

        private void SetValues(IEcsSystems systems)
        {
            var world = systems.GetWorld(Identifiers.Worlds.GameWorldName);

            var filter = world.Filter<CPlayerTag>()
                              .Inc<CHealth>()
                              .Inc<CPosition>()
                              .Inc<CRotation>()
                              .Inc<CVelocity>()
                              .End();

            var healthPool = world.GetPool<CHealth>();
            var positionPool = world.GetPool<CPosition>();
            var rotationPool = world.GetPool<CRotation>();
            var velocityPool = world.GetPool<CVelocity>();

            foreach (var entity in filter)
            {
                health.text = healthPool.Get(entity).Value.ToString();
                position.text = positionPool.Get(entity).Value.ToString();
                velocity.text = velocityPool.Get(entity).Value.magnitude.ToString();
                var rotation = rotationPool.Get(entity).Value;
                rotation.ToAngleAxis(out var angle, out _);
                this.rotation.text = angle.ToString();
            }
        }
    }
}