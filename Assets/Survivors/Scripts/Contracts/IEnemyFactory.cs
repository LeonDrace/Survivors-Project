using UnityEngine;

namespace Survivors.Scripts.Contracts
{
    public interface IEnemyFactory
    {
        void Create(Vector2 position);
    }
}