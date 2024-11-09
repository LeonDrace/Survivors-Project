using UniRx;

namespace Survivors.Enemy
{
    public class EnemyKillCounterModel
    {
        private readonly IEnemyData m_Data;
        
        public ReactiveCollection<IEnemy> Enemies => m_Data.Enemies;
        public ReactiveProperty<int> KilledEnemies => m_Data.KilledEnemies;
        
        
        public EnemyKillCounterModel(IEnemyData data)
        {
            m_Data = data;
        }
    }
}