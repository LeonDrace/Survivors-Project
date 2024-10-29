using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponBehaviorPresenter
	{
		private readonly WeaponBehaviorModel m_Model;
		public WeaponBehaviorModel Model => m_Model;

		public WeaponBehaviorPresenter(WeaponBehaviorModel model)
		{
			m_Model = model;
		}

		public void OnTick(Transform playerTransform, float deltaTime)
		{
			CooldownUpdate(playerTransform, deltaTime);
			ProjectileUpdate();
		}

		private void CooldownUpdate(Transform playerTransform, float deltaTime)
		{
			m_Model.CurrentCooldown -= deltaTime;
			m_Model.CooldownPercentage.Value = m_Model.CurrentCooldown / m_Model.DefaultCooldown;

			if (m_Model.CurrentCooldown > 0) return;

			Collider2D[] enemies = GetEnemies(playerTransform);
			if (enemies.Length == 0) return;

			m_Model.CurrentCooldown = m_Model.DefaultCooldown;
			for (int i = 0; i < m_Model.ProjectileAmount; i++)
			{
				Spawn(playerTransform.position, enemies[UnityEngine.Random.Range(0, enemies.Length)].transform.position);
			}
		}

		private void ProjectileUpdate()
		{
			for (int i = m_Model.Projectiles.Count - 1; i >= 0; i--)
			{
				if (m_Model.Projectiles[i].IsDead())
				{
					m_Model.Projectiles.RemoveAt(i);
				}
				else
				{
					m_Model.Projectiles[i].OnTick();
				}
			}
		}

		public void Spawn(Vector3 spawnPos, Vector3 targetPos)
		{
			m_Model.Projectiles.Add(m_Model.CreateProjectile(spawnPos, targetPos));
		}

		private Collider2D[] GetEnemies(Transform playerTransform)
		{
			return Physics2D.OverlapCircleAll(playerTransform.position, m_Model.Range, m_Model.LayerMask);
		}
	}
}


