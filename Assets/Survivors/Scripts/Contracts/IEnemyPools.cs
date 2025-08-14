using Survivors.Scripts.Enemies.Settings;
using UnityEngine;

namespace Survivors.Scripts.Contracts
{
    public interface IEnemyPools
    {
        IEnemyView PopEnemyView(string configId);
        IEnemyView PopEnemyView(string configId, Vector2 position, Quaternion rotation);
        void PutEnemyView(string configId, IEnemyView view);
    }
}