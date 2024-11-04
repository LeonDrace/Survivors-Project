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
			Vector2 position = GetRandomScreenBorderPointAsWorldPosition(m_Model.Camera,100,100);
			for (int i = 0; i < amount; i++)
			{
				if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1, NavMesh.AllAreas))
				{
					var enemy = m_Model.EnemyFactory.Create(m_Model.EnemySettings[Random.Range(0, m_Model.EnemySettings.Length)], position);
					m_Model.Enemies.Add(enemy);
				}
			}
		}

		private Vector2 GetRandomPointOnRadius(Vector2 position, float radius)
		{
			return Vector2.one * radius * Random.onUnitSphere + position;
		}
		
		private Vector3 GetRandomScreenBorderPointAsWorldPosition(Camera camera, float widthOffset = 0, float heightOffset = 0)
		{
			int width = Screen.width;
			int height = Screen.height;
			int randomBorder = UnityEngine.Random.Range(0, 4);

			return randomBorder switch
			{
				0 => camera.ScreenToWorldPoint(new Vector3(Random.Range(-widthOffset, width + widthOffset), -heightOffset, 0)),
				1 => camera.ScreenToWorldPoint(new Vector3(Random.Range(-widthOffset, width + widthOffset), height + heightOffset, 0)),
				2 => camera.ScreenToWorldPoint(new Vector3(-widthOffset, Random.Range(-heightOffset, height + heightOffset), 0)),
				3 => camera.ScreenToWorldPoint(new Vector3(width + heightOffset, Random.Range(heightOffset, height + heightOffset), 0)),
				_ => Vector3.zero
			};
		}
	}
}
