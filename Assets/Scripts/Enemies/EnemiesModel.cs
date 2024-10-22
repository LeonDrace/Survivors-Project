using Survivors.Data;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemiesModel
	{
		private readonly EnemyData _enemyData;

		public EnemiesPresenter.SpawnSettings SpawnSettings { get; private set; }
		public EnemyModel.Settings[] EnemySettings { get; private set; }
		public EnemyPresenter.Factory EnemyFactory { get; private set; }
		public Transform PlayerTransform { get; private set; }
		public Camera Camera { get; private set; }
		public float RandomSpawnCooldown { get; set; }
		public ReactiveProperty<int> KilledEnemies => _enemyData.KilledEnemies;
		public ReactiveCollection<IEnemy> Enemies => _enemyData.Enemies;

		public EnemiesModel(
			EnemiesPresenter.SpawnSettings spawnSettings,
			EnemyModel.Settings[] enemySettings,
			EnemyData enemyData,
			PlayerData playerData,
			EnemyPresenter.Factory factory,
			PlayerView playerView)
		{
			_enemyData = enemyData;
			_enemyData.KilledEnemies = new ReactiveProperty<int>(0);
			SpawnSettings = spawnSettings;
			EnemySettings = enemySettings;
			EnemyFactory = factory;
			PlayerTransform = playerData.Transform;
			Camera = Camera.main;
		}
	}
}
