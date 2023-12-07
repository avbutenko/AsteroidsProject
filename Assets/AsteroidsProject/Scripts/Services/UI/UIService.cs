using AsteroidsProject.Shared;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace AsteroidsProject.Services
{
    public class UIService : IUIService, IDisposable
    {
        private readonly List<IUIScreenFactoryAsync> factories;
        private List<IUIScreenPresenter> presenters;

        public UIService(IEnumerable<IUIScreenFactoryAsync> bindedFactories)
        {
            factories = new();

            foreach (var factory in bindedFactories)
            {
                factories.Add(factory);
            }
        }

        public async UniTask PreLoadUI()
        {
            presenters = new();

            foreach (var factory in factories)
            {
                var presenter = await factory.CreateAsync();
                presenters.Add(presenter);
            }
        }

        public T Get<T>() where T : IUIScreenPresenter
        {
            return (T)presenters.Find(x => x is T);
        }

        public void Dispose()
        {
            factories.Clear();
            presenters.Clear();
        }
    }
}