using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IEnemyFactory
    {
        void Create(Vector2 position);
    }
}