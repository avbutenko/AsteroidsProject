﻿using AsteroidsProject.Shared;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace AsteroidsProject.Services
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IAssetProvider assetProvider;
        public ConfigProvider(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public async Task<T> Load<T>(string configAddress) where T : class
        {
            var asset = await assetProvider.Load<TextAsset>(configAddress);
            return JsonConvert.DeserializeObject<T>(asset.text);
        }
    }
}