using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IEnemyView
    {
        int Id { get; set; }
        Transform Transform { get; }

        void Initialize(int id, [NotNull] Action<int, float> onTakeDamage, [CanBeNull] Action<Vector2> onDespawn);
        void OnDespawn();
        void Dispose();
        void OnTick();
        void UpdatePositionAndRotation(Vector2 velocity, Quaternion rotation);
        void UpdateVelocityAndRotation(Vector2 velocity, Quaternion rotation);
        void SetState(bool state);
    }
}