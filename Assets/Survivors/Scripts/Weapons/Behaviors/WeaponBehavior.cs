using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Weapons
{
	public class WeaponBehavior
	{
		protected readonly WeaponSetting m_WeaponSetting;
		protected readonly ProjectileFactory m_ProjectileFactory;

		protected List<IProjectile> m_Projectiles = new List<IProjectile>();
		private float m_Cooldown = 0;
		private LayerMask m_LayerMask = 1 << 6;

		public WeaponSetting WeaponSetting => m_WeaponSetting;
		public ReactiveProperty<float> Cooldown = new ReactiveProperty<float>(0);

		public Guid Guid { get; private set; } = System.Guid.NewGuid();

		public WeaponBehavior(WeaponSetting weaponSetting, ProjectileFactory projectileFactory)
		{
			m_WeaponSetting = weaponSetting;
			m_ProjectileFactory = projectileFactory;
		}

		public void OnTick(Transform playerTransform, float deltaTime)
		{
			CooldownUpdate(playerTransform, deltaTime);
			ProjectileUpdate();
		}

		private void CooldownUpdate(Transform playerTransform, float deltaTime)
		{
			m_Cooldown -= deltaTime;
			Cooldown.Value = m_Cooldown / m_WeaponSetting.Cooldown;

			if (m_Cooldown > 0) return;

			Collider2D[] enemies = GetEnemies(playerTransform);
			if (enemies.Length == 0) return;

			m_Cooldown = m_WeaponSetting.Cooldown;
			for (int i = 0; i < m_WeaponSetting.Projectiles; i++)
			{
				Spawn(playerTransform.position, enemies[UnityEngine.Random.Range(0, enemies.Length)].transform.position);
			}
		}

		private void ProjectileUpdate()
		{
			for (int i = m_Projectiles.Count - 1; i >= 0; i--)
			{
				if (m_Projectiles[i].IsDead())
				{
					m_Projectiles.RemoveAt(i);
				}
				else
				{
					m_Projectiles[i].OnTick();
				}
			}
		}

		public void Spawn(Vector3 spawnPos, Vector3 targetPos)
		{
			var projectile = m_ProjectileFactory.Create(spawnPos, targetPos, m_WeaponSetting);
			m_Projectiles.Add(projectile);
		}

		private Collider2D[] GetEnemies(Transform playerTransform)
		{
			return Physics2D.OverlapCircleAll(playerTransform.position, m_WeaponSetting.Range, m_LayerMask);
		}

		public class Factory : PlaceholderFactory<WeaponSetting, WeaponBehavior> { }
	}
}


