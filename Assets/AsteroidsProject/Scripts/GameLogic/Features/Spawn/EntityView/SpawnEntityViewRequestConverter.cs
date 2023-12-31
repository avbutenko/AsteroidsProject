﻿using AsteroidsProject.Shared;
using Newtonsoft.Json.Linq;

namespace AsteroidsProject.GameLogic.Features.Spawn
{
    public class SpawnEntityViewRequestConverter : IComponentConverter
    {
        public string TokenName => nameof(CSpawnEntityViewRequest);

        public object Convert(JToken token)
        {
            return new CSpawnEntityViewRequest { PrefabAddress = (string)token };
        }
    }
}