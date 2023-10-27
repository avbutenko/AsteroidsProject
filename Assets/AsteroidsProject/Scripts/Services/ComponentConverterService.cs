using AsteroidsProject.Shared;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace AsteroidsProject.Services
{
    public class ComponentConverterService : IComponentConverterService
    {
        private List<IComponentConverter> converters;

        [Inject]
        public void Construct(IEnumerable<IComponentConverter> converters)
        {
            this.converters = converters.ToList();
        }

        public List<IComponentConverter> Converters => converters;
    }
}