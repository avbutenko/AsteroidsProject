using AsteroidsProject.GameLogic.EntryPoints;
using AsteroidsProject.Services;
using AsteroidsProject.Shared;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private EcsUguiEmitter uguiEmitter;

        public override void InstallBindings()
        {
            Container.Bind<EcsUguiEmitter>().FromComponentInNewPrefab(uguiEmitter).AsSingle();
            Container.BindInterfacesAndSelfTo<EcsSystemsFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<EcsSystemListProvider>()
                     .FromSubContainerResolve()
                     .ByInstaller<MainMenuSceneSystemsInstaller>()
                     .AsSingle();

            Container.Bind<IEcsSystemsRunner>().To<EcsSystemsRunner>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuSceneEntryPoint>().AsSingle();
        }
    }
}