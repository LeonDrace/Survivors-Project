using Survivors.Features.Enemies.Components;
using Survivors.Features.Enemies.Contexts;

namespace Survivors.Features.Contracts
{
    public interface IEnemyComponentManager
    {
        int Count { get; }
        int GetId { get; }

        void AddEnemy(in EnemyComponentContext context);
        void RemoveEnemy(int id);
        void Clear();


        void ChangeHealth(int id, float change);

        void AddParticle(IParticleManager particleManager);
    }
}