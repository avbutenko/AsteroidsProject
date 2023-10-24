using AsteroidsProject.Shared;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace AsteroidsProject.Services
{
    public class ComponentConverterService : IComponentConverterService
    {
        private List<IJTokenConverter> converters;

        [Inject]
        public void Construct(IEnumerable<IJTokenConverter> converters)
        {
            this.converters = converters.ToList();
        }

        public List<IJTokenConverter> Converters => converters;
    }
}