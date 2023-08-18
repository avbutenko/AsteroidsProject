using System.Threading.Tasks;
namespace AsteroidsProject.CodeBase.Infrastructure
{
    public interface IFactory
    {
        public Task Create();
    }
}
