﻿using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface IPool
    {
        public void Register(GameObject prefab, GameObject instance);
        public bool HasObjects(GameObject prefab);
        public void Push(GameObject instance);
        public GameObject Pull(GameObject prefab);
    }
}