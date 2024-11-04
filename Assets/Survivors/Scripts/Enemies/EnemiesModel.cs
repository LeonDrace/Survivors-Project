using Survivors.Data;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemiesModel : IEnemyData
	{
		public SpawnSettings SpawnSettings { get; private set; }
		public EnemySettings[] EnemySettings { get; private set; }
		public EnemyFactory EnemyFactory { get; private set; }
		public Transform PlayerTransform { get; private set; }
		public float RandomSpawnCooldown { get; set; }
		public ReactiveProperty<int> KilledEnemies { get; set; }
		public ReactiveCollection<IEnemy> Enemies { get; set; } = new();
		public Camera Camera { get; private set; }

		public EnemiesModel(
			SpawnSettings spawnSettings,
			EnemySettings[] enemySettings,
			PlayerData playerData,
			EnemyFactory factory)
		{
			KilledEnemies = new ReactiveProperty<int>(0);
			SpawnSettings = spawnSettings;
			EnemySettings = enemySettings;
			EnemyFactory = factory;
			PlayerTransform = playerData.Transform;
			Camera = Camera.main;
		}
	}
}
