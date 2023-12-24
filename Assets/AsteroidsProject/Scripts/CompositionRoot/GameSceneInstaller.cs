﻿using Zenject;
using AsteroidsProject.Shared;
using AsteroidsProject.Services;
using AsteroidsProject.MonoBehaviours;
using AsteroidsProject.GameLogic.EntryPoint.GameScene;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;

namespace AsteroidsProject.CompositionRoot
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private EcsUguiEmitter uguiEmitter;
        [SerializeField] private GameSceneData sceneData;

        public override void InstallBindings()
        {
            Container.BindInstance(uguiEmitter).AsSingle();
            Container.Bind<IGameSceneData>().FromInstance(sceneData).AsSingle();
            Container.Bind<ILevelService>().To<LevelService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameObjectPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActiveGOMappingService>().AsSingle();
            Container.Bind<IEntityGameObjectFactory>().To<EntityGameObjectFactory>().AsSingle();
            Container.Bind(typeof(IEcsSystemsRunner), typeof(IRestartable)).To<EcsSystemsRunner>().AsSingle();
            Container.BindInterfacesAndSelfTo<RestartService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSceneEntryPoint>().AsSingle();
        }
    }
}