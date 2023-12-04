using AsteroidsProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace AsteroidsProject.Services
{
    public class ComponentConverterService : IComponentConverterService, IDisposable
    {
        private List<IComponentConverter> converters;

        [Inject]
        public void Construct(IEnumerable<IComponentConverter> converters)
        {
            this.converters = converters.ToList();
        }

        public void Dispose()
        {
            converters.Clear();
        }

        public List<IComponentConverter> Converters => converters;
    }
}