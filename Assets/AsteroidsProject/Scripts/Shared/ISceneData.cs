using UnityEngine;

namespace AsteroidsProject.Shared
{
    public interface ISceneData
    {
        public Transform SpawnPlayerPosition { get; }
        public Transform AsteroidsParent { get; }
    }
}