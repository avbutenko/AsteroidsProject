using System.Collections.Generic;
namespace AsteroidsProject.Shared
{
    public interface IComponentConverterService
    {
        public List<IComponentConverter> Converters { get; }
    }
}