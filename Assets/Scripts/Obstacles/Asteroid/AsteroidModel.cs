using AsteroidsProject.Infrastructure.Obstacles.Asteroid;
using Codice.CM.Client.Differences;
using System;
using UnityEngine;

public class AsteroidModel : IAsteroidModel
{
    public event Action<Quaternion> RotationChanged;
    public event Action<Vector3> PositionChanged;
    public event Action<float> ScaleChanged;

    private Vector3 position;
    private Quaternion rotation;
    private float scale;
    private float rotationSpeed;
    private float movementSpeed;
    private Vector3 movementDirection;

    public Vector3 Position
    {
        get { return position; }
        set
        {
            position = value;
            PositionChanged?.Invoke(position);
        }
    }

    public Quaternion Rotation
    {
        get { return rotation; }
        set
        {
            rotation = value;
            RotationChanged?.Invoke(rotation);
        }
    }

    public float Scale
    {
        get { return scale; }
        set
        {
            scale = value;
            ScaleChanged?.Invoke(scale);
        }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float RatationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }

    public Vector3 MovementDirection
    {
        get { return movementDirection; }
        set { movementDirection = value; }
    }

    public int RotationDirection { get; set; }
}
