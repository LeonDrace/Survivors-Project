using JetBrains.Annotations;
using Survivors.Features.Contracts;
using UniRx;

namespace Survivors.Features.UI.Kill_Count
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