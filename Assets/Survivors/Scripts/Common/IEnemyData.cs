using UniRx;

namespace Survivors.Enemy
{
    public interface IEnemyData
    {
        public ReactiveProperty<int> KilledEnemies { get; set; }
        public ReactiveCollection<IEnemy> Enemies { get; set; }
    }
}