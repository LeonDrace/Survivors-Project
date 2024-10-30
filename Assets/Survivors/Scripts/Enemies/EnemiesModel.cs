using Survivors.Data;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemiesModel
	{
		private readonly EnemyData m_EnemyData;

		public SpawnSettings SpawnSettings { get; private set; }
		public EnemySettings[] EnemySettings { get; private set; }
		public EnemyFactory EnemyFactory { get; private set; }
		public Transform PlayerTransform { get; private set; }
		public float RandomSpawnCooldown { get; set; }
		public ReactiveProperty<int> KilledEnemies => m_EnemyData.KilledEnemies;
		public ReactiveCollection<IEnemy> Enemies => m_EnemyData.Enemies;

		public EnemiesModel(
			SpawnSettings spawnSettings,
			EnemySettings[] enemySettings,
			EnemyData enemyData,
			PlayerData playerData,
			EnemyFactory factory)
		{
			m_EnemyData = enemyData;
			m_EnemyData.KilledEnemies = new ReactiveProperty<int>(0);
			SpawnSettings = spawnSettings;
			EnemySettings = enemySettings;
			EnemyFactory = factory;
			PlayerTransform = playerData.Transform;
		}
	}
}
