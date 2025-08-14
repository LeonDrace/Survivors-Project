using UnityEngine;

namespace Survivors.Features.Enemies.Components
{
    public struct EnemyTransform
    {
        public Vector2 Position;
        public Quaternion Rotation;

        public EnemyTransform(Vector2 position)
        {
            Position = position;
            Rotation = Quaternion.identity;
        }

        public void Populate(Vector2 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}