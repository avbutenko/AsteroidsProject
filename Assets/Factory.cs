using AsteroidsProject.CodeBase.Infrastructure;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.Assets
{
    public class Factory : AsteroidsProject.CodeBase.Infrastructure.IFactory
    {
        private readonly DiContainer diContainer;
        private readonly IAssetProvider assetProvider;

        public Factory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            this.diContainer = diContainer;
            this.assetProvider = assetProvider;
        }
        public async Task Create()
        {
            var prefab = await assetProvider.LoadAsync<GameObject>("Player/Prefabs/Player.prefab");
            var createdGo = diContainer.InstantiatePrefab(prefab);
        }
    }
}