using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IAssetProvider
    {
        public Task<T> Load<T>(string address) where T : Object;
    }
}
