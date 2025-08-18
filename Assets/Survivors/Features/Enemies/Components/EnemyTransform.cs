using UnityEngine;

namespace Survivors.Features.Enemies.Components
{
    public struct EnemyTransform
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Quaternion Rotation;

        public EnemyTransform(Vector2 position)
        {
            Position = position;
            Velocity = Vector2.zero;
            Rotation = Quaternion.identity;
        }

        public void Populate(Vector2 position, Vector2 velocity, Quaternion rotation)
        {
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
        }
    }
}