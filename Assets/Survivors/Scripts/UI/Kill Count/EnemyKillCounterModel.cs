using JetBrains.Annotations;
using Survivors.Scripts.Contracts;
using UniRx;

namespace Survivors.Enemy
{
    [UsedImplicitly]
    public class EnemyKillCounterModel
    {
        private readonly IEnemies m_Data;


        public EnemyKillCounterModel(IEnemies data)
        {
            m_Data = data;
        }

        public ReactiveProperty<int> KilledEnemies => m_Data.KilledEnemies;
    }
}