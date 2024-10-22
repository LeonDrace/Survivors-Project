using Survivors.Data;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemiesModel
	{
		private readonly EnemyData m_EnemyData;

		public EnemiesPresenter.SpawnSettings SpawnSettings { get; private set; }
		public EnemyModel.Settings[] EnemySettings { get; private set; }
		public EnemyPresenter.Factory EnemyFactory { get; private set; }
		public Transform PlayerTransform { get; private set; }
		public Camera Camera { get; private set; }
		public float RandomSpawnCooldown { get; set; }
		public ReactiveProperty<int> KilledEnemies => m_EnemyData.KilledEnemies;
		public ReactiveCollection<IEnemy> Enemies => m_EnemyData.Enemies;

		public EnemiesModel(
			EnemiesPresenter.SpawnSettings spawnSettings,
			EnemyModel.Settings[] enemySettings,
			EnemyData enemyData,
			PlayerData playerData,
			EnemyPresenter.Factory factory,
			PlayerView playerView)
		{
			m_EnemyData = enemyData;
			m_EnemyData.KilledEnemies = new ReactiveProperty<int>(0);
			SpawnSettings = spawnSettings;
			EnemySettings = enemySettings;
			EnemyFactory = factory;
			PlayerTransform = playerData.Transform;
			Camera = Camera.main;
		}
	}
}
