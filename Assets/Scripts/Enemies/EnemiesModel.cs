
using UniRx;
using static Scripts.EnemiesPresenter;

namespace Scripts
{
	public class EnemiesModel
	{
		public SpawnSettings SpawnSettings { get; private set; }
		public EnemyView.Settings[] EnemySettings { get; private set; }
		public EnemyView.Factory EnemyFactory { get; private set; }
		public PlayerView PlayerView { get; private set; }
		public ReactiveProperty<int> KilledEnemies { get; private set; }

		public EnemiesModel(SpawnSettings spawnSettings, EnemyView.Settings[] enemySettings, EnemyView.Factory factory, PlayerView playerView)
		{
			KilledEnemies = new ReactiveProperty<int>(0);
			SpawnSettings = spawnSettings;
			EnemySettings = enemySettings;
			EnemyFactory = factory;
			PlayerView = playerView;
		}
	}
}
