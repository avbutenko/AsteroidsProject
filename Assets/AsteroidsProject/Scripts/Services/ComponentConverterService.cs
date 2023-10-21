using AsteroidsProject.Shared;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidsProject.Services
{
    public class ComponentConverterService : IComponentConverterService
    {
        private readonly List<IJTokenConverter> converters;

        public ComponentConverterService(IEnumerable<IJTokenConverter> converters)
        {
            this.converters = converters.ToList();
        }

        public List<IJTokenConverter> Converters => converters;
    }
}