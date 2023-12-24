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
            Container.BindInstance(uguiEmitter).AsSingle();
            Container.Bind(typeof(IEcsSystemsRunner), typeof(IRestartable)).To<EcsSystemsRunner>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuSceneEntryPoint>().AsSingle();
        }
    }
}