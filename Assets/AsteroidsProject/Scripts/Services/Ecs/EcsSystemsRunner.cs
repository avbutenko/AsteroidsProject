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
        private readonly IEcsSystemsFactory systemsFactory;
        private readonly ITimeService timeService;
        private EcsWorld gameWorld;
        private List<IEcsSystem> updateSystemsList;
        private List<IEcsSystem> fixedUpdateSystemsList;
        private List<IEcsSystem> uguiSystemsList;
        private EcsSystems fixedUpdateSystems;
        private EcsSystems updateSystems;
        private EcsSystems uguiSystems;

        public EcsSystemsRunner(IEcsSystemsFactory systemsFactory, ITimeService timeService)
        {
            this.systemsFactory = systemsFactory;
            this.timeService = timeService;
        }

        public EcsUguiEmitter UIRoot
        {
            get => uguiEmitter;
            set => uguiEmitter = value;
        }

        public void PreInitSystems(List<string> updateSystemNameList, List<string> fixedUpdateSystemNameList, List<string> uguiSystemNameList)
        {
            updateSystemsList = InitSystems(updateSystemNameList);
            fixedUpdateSystemsList = InitSystems(fixedUpdateSystemNameList);
            uguiSystemsList = InitSystems(uguiSystemNameList);
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
            updateSystems = ConvertSystemList(updateSystemsList, gameWorld);
#if UNITY_EDITOR
            updateSystems?.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
            updateSystems?.Init();
            fixedUpdateSystems = ConvertSystemList(fixedUpdateSystemsList, gameWorld);
            fixedUpdateSystems?.Init();
        }

        private void InitGUIWorldSystems()
        {
            uguiSystems = ConvertSystemList(uguiSystemsList, new EcsWorld());
            uguiSystems.AddWorld(gameWorld, Identifiers.Worlds.GameWorldName);
            uguiSystems.InjectUgui(uguiEmitter).Init();
        }

        private List<IEcsSystem> InitSystems(List<string> systemNameList)
        {
            if (systemNameList == null)
            {
                return null;
            }
            else
            {
                var systemList = new List<IEcsSystem>();

                foreach (var systemName in systemNameList)
                {
                    systemList.Add(systemsFactory.Create(systemName));
                }

                return systemList;
            }
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