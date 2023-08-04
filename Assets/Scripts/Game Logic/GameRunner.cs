using AsteroidsProject.Infrastructure.Units.Ship;
using UnityEngine;
using Zenject;

namespace AsteroidsProject.GameLogic
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Transform startPosition;

        private GameBootstrapper.Factory gameBootstrapperFactory;
        private IShipPresenterFactory shipFactory;

        [Inject]
        public void Construct(GameBootstrapper.Factory bootstrapperFactory, IShipPresenterFactory shipFactory)
        {
            this.gameBootstrapperFactory = bootstrapperFactory;
            this.shipFactory = shipFactory;
        }

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;

            gameBootstrapperFactory.Create();
        }

        private void Start()
        {
            SpawnPlayerShip();
        }

        private void SpawnPlayerShip()
        {
            Transformable initparams = new()
            {
                Position = startPosition.position,
                Rotation = startPosition.rotation,
                Scale = Vector3.one[0],
            };

            shipFactory.Create(initparams);
        }
    }
}