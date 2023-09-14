using AsteroidsProject.Shared;
using UnityEngine;

namespace AsteroidsProject.MonoBehaviours
{
    public class GameplayObjectView : MonoBehaviour, ILinkToGameplayObjectView
    {
        public Vector2 Position { set => transform.localPosition = value; }
        public Quaternion Rotation { set => transform.localRotation = value; }
        public float Scale { set => transform.localScale = new Vector3(value, value, value); }
    }
}