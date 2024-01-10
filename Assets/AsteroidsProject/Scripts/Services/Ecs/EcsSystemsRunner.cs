using AsteroidsProject.Shared;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Unity.Ugui;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class EcsSystemsRunner : IEcsSystemsRunner
    {
        private EcsUguiEmitter uguiEmitter;
        private readonly ITimeService timeService;
        private readonly IEcsSystemListProvider systemListProvider;
        private EcsWorld gameWorld;
        private EcsSystems fixedUpdateSystems;
        private EcsSystems updateSystems;
        private EcsSystems uguiSystems;

        public EcsSystemsRunner(ITimeService timeService, IEcsSystemListProvider systemListProvider, EcsUguiEmitter uiRoot)
        {
            this.timeService = timeService;
            this.systemListProvider = systemListProvider;
            this.uguiEmitter = uiRoot;
        }

        public void Initialize()
        {
            InitGameWorldSystems();
            InitGUIWorldSystems();
        }

        public void Tick()
        {
            if (!timeService.IsPaused)
            {
                updateSystems?.Run();
            }

            uguiSystems?.Run();
        }

        public void FixedTick()
        {
            if (!timeService.IsPaused)
            {
                fixedUpdateSystems?.Run();
            }
        }

        public void Restart()
        {
            ClearSystems(updateSystems);
            ClearSystems(fixedUpdateSystems);
            InitGameWorldSystems();
            uguiSystems.GetAllNamedWorlds()[Identifiers.Worlds.GameWorldName] = gameWorld;
        }

        public virtual void Dispose()
        {
            EcsPhysicsEvents.ecsWorld = null;
            ClearSystems(updateSystems);
            ClearSystems(fixedUpdateSystems);
            ClearSystems(uguiSystems);
        }

        private void InitGameWorldSystems()
        {
            gameWorld = new EcsWorld();
            EcsPhysicsEvents.ecsWorld = gameWorld;
            updateSystems = ConvertSystemList(systemListProvider.UpdateSystemList, gameWorld);
#if UNITY_EDITOR
            updateSystems?.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
            updateSystems?.Init();
            fixedUpdateSystems = ConvertSystemList(systemListProvider.FixedUpdateSystemList, gameWorld);
            fixedUpdateSystems?.Init();
        }

        private void InitGUIWorldSystems()
        {
            uguiSystems = ConvertSystemList(systemListProvider.UguiSystemList, new EcsWorld());
            uguiSystems.AddWorld(gameWorld, Identifiers.Worlds.GameWorldName);
            uguiSystems.InjectUgui(uguiEmitter).Init();
        }

        private EcsSystems ConvertSystemList(List<IEcsSystem> systemsList, EcsWorld world)
        {
            if (systemsList == null)
            {
                return null;
            }
            else
            {
                var systems = new EcsSystems(world);

                foreach (var system in systemsList)
                {
                    systems.Add(system);
                }

                return systems;
            }
        }

        private void ClearSystems(EcsSystems systems)
        {
            if (systems != null)
            {
                systems.Destroy();
                systems.GetWorld().Destroy();
            }
        }
    }
}