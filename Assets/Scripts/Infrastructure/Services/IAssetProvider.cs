using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidsProject.Infrastructure.Services
{
    public interface IAssetProvider
    {
        public Task<TAsset> Load<TAsset>(string address) where TAsset : Object;
    }
}
