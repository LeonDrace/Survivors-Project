using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Survivors.Enemy
{
	public class EnemiesPresenter : ITickable
	{
		private readonly EnemiesModel m_Model;

		private float _randomSpawnCooldown = 0;

		public EnemiesPresenter(EnemiesModel enemiesModel, CompositeDisposable disposables)
		{
			m_Model = enemiesModel;
		}

		public void Tick()
		{
			SpawnerTick();
			EnemiesTick();
		}

		private void EnemiesTick()
		{
			int count = m_Model.Enemies.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				var enemy = m_Model.Enemies[i];
				enemy.OnTick();

				if (enemy.IsDead())
				{
					enemy.Destroy();
					m_Model.Enemies.RemoveAt(i);
				}
			}
		}

		private void SpawnerTick()
		{
			_randomSpawnCooldown -= Time.deltaTime;

			if (_randomSpawnCooldown <= 0)
			{
				_randomSpawnCooldown = m_Model.SpawnSettings.GetRandomSpawnCooldown();
				SpawnEnemies();
			}
		}

		private void SpawnEnemies()
		{
			if (m_Model.Enemies.Count >= m_Model.SpawnSettings.MaxSpawnAmount)
			{
				return;
			}

			int amount = m_Model.SpawnSettings.GetSpawnAmount();
			float radius = m_Model.SpawnSettings.Radius;
			Vector2 playerPosition = m_Model.PlayerTransform.position;
			Vector2 position = Vector2.one * Random.onUnitSphere * radius + playerPosition;
			for (int i = 0; i < amount; i++)
			{
				if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1, NavMesh.AllAreas))
				{
					var enemy = m_Model.EnemyFactory.Create(position, m_Model.EnemySettings[Random.Range(0, m_Model.EnemySettings.Length)]);
					m_Model.Enemies.Add(enemy);
				}
			}
		}

		public static Rect GetScreenWorldRect(Camera value)
		{
			Vector3 bottomLeft = value.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
			Vector3 topRight = value.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
			return (new Rect(bottomLeft.x, bottomLeft.y, topRight.x * 2f, topRight.y * 2f));
		}
	}
}
