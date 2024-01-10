using AsteroidsProject.GameLogic.Features.UI;
using AsteroidsProject.Services;
using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidsProject.CompositionRoot
{
    public class MainMenuSceneSystemsInstaller : Installer<MainMenuSceneSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsSystemListProvider>()
                     .AsSingle()
                     .WithArguments(SetUpdateSystems(), SetFixedUpdateSystems(), SetGUISystems());
        }

        private List<Type> SetUpdateSystems()
        {
            return new List<Type>
            {
                typeof(InitMainMenuSceneUISystem),
            };
        }

        private List<Type> SetFixedUpdateSystems()
        {
            return null;
        }

        private List<Type> SetGUISystems()
        {
            return new List<Type>
            {
                typeof(MainMenuScreenControlSystem),
            };
        }
    }
}