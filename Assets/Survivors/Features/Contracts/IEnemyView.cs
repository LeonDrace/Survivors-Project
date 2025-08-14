using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IEnemyView
    {
        int Index { get; set; }
        Transform Transform { get; }

        void Initialize(IEnemyManager enemyManager, int index);
        void OnDespawn();
        void Dispose();
        void OnTick();
        void UpdatePositionAndRotation(Vector2 position, Quaternion rotation);
        void SetState(bool state);
    }
}