using System.Threading.Tasks;

namespace AsteroidsProject.Shared
{
    public interface IConfigProvider
    {
        public string GameConfigPath { get; }
        public Task<T> Load<T>(string configAddress) where T : class;
    }
}