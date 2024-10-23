using Survivors.Enemy;
using UniRx;

namespace Survivors.Data
{
	public class EnemyData
	{
		public ReactiveProperty<int> KilledEnemies { get; set; }
		public ReactiveCollection<IEnemy> Enemies { get; set; } = new ReactiveCollection<IEnemy>();
	}
}
