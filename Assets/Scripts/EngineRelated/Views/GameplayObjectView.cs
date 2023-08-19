using AsteroidsProject.Infrastructure.Views;
using UnityEngine;

namespace AsteroidsProject.EngineRelated.Views
{
    public class GameplayObjectView : MonoBehaviour, IGameplayObjectView
    {
        public Transform Transform => transform;
    }
}