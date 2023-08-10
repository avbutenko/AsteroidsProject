using AsteroidsProject.Infrastructure.Units.Ship;
using UnityEngine;

namespace AsteroidsProject.Data.Units.Ship
{
    public class ShipInitParams : IShipInitParams
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
    }
}