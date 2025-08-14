using UnityEngine;

namespace Survivors.Scripts.Contracts
{
    public interface IEnemyView
    {
        int Index { get; set; }
        Transform Transform { get; }

        void Initialize(IEnemyManager enemyManager, int index);
        void Dispose();
        void OnTick();
        void UpdatePositionAndRotation(Vector2 position, Quaternion rotation);
    }
}