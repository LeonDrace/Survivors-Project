using Survivors.Scripts.Enemies.Components;

namespace Survivors.Scripts.Contracts
{
    public interface IEnemyManager
    {
        void AddSharedData(ISharedEnemyData[] sharedData);
        void AddEnemy(IEnemyView view, in EnemyTransform transform, in EnemyVitals vitals, int sharedDataIndex);
        void RemoveEnemy(int index);
        void Clear();
        int Count { get; }
        void ChangeHealth(int index, float change);
    }
}