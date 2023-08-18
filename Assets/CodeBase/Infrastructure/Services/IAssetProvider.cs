using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.CodeBase.Infrastructure
{
    public interface IAssetProvider
    {
        public Task<T> LoadAsync<T>(string address) where T : Object;
    }
}
