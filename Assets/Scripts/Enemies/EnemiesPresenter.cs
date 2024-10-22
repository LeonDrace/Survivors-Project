using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Survivors.Enemy
{
	public class EnemiesPresenter : ITickable
	{
		private readonly EnemiesModel _model;

		private float _randomSpawnCooldown = 0;

		public EnemiesPresenter(EnemiesModel enemiesModel, CompositeDisposable disposables)
		{
			_model = enemiesModel;
		}

		public void Tick()
		{
			SpawnerTick();
			EnemiesTick();
		}

		private void EnemiesTick()
		{
			int count = _model.Enemies.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				var enemy = _model.Enemies[i];
				enemy.OnTick();

				if (enemy.IsDead())
				{
					enemy.Destroy();
					_model.Enemies.RemoveAt(i);
				}
			}
		}

		private void SpawnerTick()
		{
			_randomSpawnCooldown -= Time.deltaTime;

			if (_randomSpawnCooldown <= 0)
			{
				_randomSpawnCooldown = _model.SpawnSettings.GetRandomSpawnCooldown();
				SpawnEnemies();
			}
		}

		private void SpawnEnemies()
		{
			if (_model.Enemies.Count >= _model.SpawnSettings.MaxSpawnAmount)
			{
				return;
			}

			int amount = _model.SpawnSettings.GetSpawnAmount();
			float radius = _model.SpawnSettings.Radius;
			Vector2 playerPosition = _model.PlayerTransform.position;
			Vector2 position = Vector2.one * Random.onUnitSphere * radius + playerPosition;
			for (int i = 0; i < amount; i++)
			{
				if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1, NavMesh.AllAreas))
				{
					var enemy = _model.EnemyFactory.Create(position, _model.EnemySettings[Random.Range(0, _model.EnemySettings.Length)]);
					_model.Enemies.Add(enemy);
				}
			}
		}

		public static Rect GetScreenWorldRect(Camera value)
		{
			Vector3 bottomLeft = value.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
			Vector3 topRight = value.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
			return (new Rect(bottomLeft.x, bottomLeft.y, topRight.x * 2f, topRight.y * 2f));
		}

		[System.Serializable]
		public class SpawnSettings
		{
			[field: SerializeField]
			private Vector2 SpawnIntervalRange { get; set; } = new UnityEngine.Vector2(0.5f, 1.5f);
			[field: SerializeField]
			private Vector2Int SpawAmountRange { get; set; } = new Vector2Int(1, 5);
			[field: SerializeField]
			public float Radius { get; private set; } = 20;
			[field: SerializeField]
			public int MaxSpawnAmount { get; private set; }

			public int GetSpawnAmount()
			{
				return Random.Range(SpawAmountRange.x, SpawAmountRange.y);
			}

			public float GetRandomSpawnCooldown()
			{
				return Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
			}
		}
	}
}
