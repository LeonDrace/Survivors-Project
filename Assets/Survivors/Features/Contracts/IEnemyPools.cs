using UnityEngine;

namespace Survivors.Features.Contracts
{
    public interface IEnemyPools
    {
        IEnemyView PopEnemyView(string configId);
        IEnemyView PopEnemyView(string configId, Vector2 position, Quaternion rotation);
        void PutEnemyView(string configId, IEnemyView view);

        IParticleManager PopEnemyDeathParticles(string configId, Vector2 position);
        void PutEnemyDeathParticles(string configId, IParticleManager enemyView);
    }
}