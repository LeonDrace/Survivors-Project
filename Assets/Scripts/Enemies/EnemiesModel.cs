using Assets.Scripts.Enemies;
using Scripts;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemiesModel
	{
		public EnemiesPresenter.SpawnSettings SpawnSettings { get; private set; }
		public EnemyModel.Settings[] EnemySettings { get; private set; }
		public EnemyPresenter.Factory EnemyFactory { get; private set; }
		public PlayerView PlayerView { get; private set; }
		public ReactiveProperty<int> KilledEnemies { get; private set; }
		public Camera Camera { get; private set; }
		public float RandomSpawnCooldown { get; set; }
		public ReactiveCollection<EnemyPresenter> Enemies { get; private set; } = new ReactiveCollection<EnemyPresenter>();

		public EnemiesModel(EnemiesPresenter.SpawnSettings spawnSettings, EnemyModel.Settings[] enemySettings, EnemyPresenter.Factory factory, PlayerView playerView)
		{
			KilledEnemies = new ReactiveProperty<int>(0);
			SpawnSettings = spawnSettings;
			EnemySettings = enemySettings;
			EnemyFactory = factory;
			PlayerView = playerView;
			Camera = Camera.main;
		}
	}
}
