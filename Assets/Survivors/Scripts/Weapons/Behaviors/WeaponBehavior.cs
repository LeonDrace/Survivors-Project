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
		private float _cooldown = 0;
		private LayerMask _layerMask = 1 << 6;

		public WeaponSetting WeaponSetting => m_WeaponSetting;
		public ReactiveProperty<float> Cooldown = new ReactiveProperty<float>(0);

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
			_cooldown -= deltaTime;
			Cooldown.Value = _cooldown / m_WeaponSetting.Cooldown;

			if (_cooldown <= 0)
			{
				Collider2D[] enemies = GetEnemies(playerTransform);
				if (enemies.Length > 0)
				{
					_cooldown = m_WeaponSetting.Cooldown;
					for (int i = 0; i < m_WeaponSetting.Projectiles; i++)
					{
						Spawn(playerTransform, enemies);
					}
				}
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

		public void Spawn(Transform playerTransform, Collider2D[] enemies)
		{
			Collider2D selectedTarget = enemies[Random.Range(0, enemies.Length)];
			var projectile = m_ProjectileFactory.Create(playerTransform.position, selectedTarget.transform.position, m_WeaponSetting);
			m_Projectiles.Add(projectile);
		}

		private Collider2D[] GetEnemies(Transform playerTransform)
		{
			return Physics2D.OverlapCircleAll(playerTransform.position, m_WeaponSetting.Range, _layerMask);
		}

		public class Factory : PlaceholderFactory<WeaponSetting, WeaponBehavior> { }
	}
}


