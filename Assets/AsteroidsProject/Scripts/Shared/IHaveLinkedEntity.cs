using Leopotam.EcsLite;

namespace AsteroidsProject.Shared
{
    public interface IHaveLinkedEntity
    {
        public EcsPackedEntity LinkedEntity { get; set; }
    }
}