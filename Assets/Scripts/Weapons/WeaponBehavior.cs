using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class WeaponBehavior
	{
		private readonly WeaponSetting m_WeaponSetting;
		private readonly ProjectileView.Factory m_ProjectileFactory;

		private float _cooldown = 0;
		private LayerMask _layerMask = 1 << 6;

		public WeaponSetting WeaponSetting => m_WeaponSetting;
		public ReactiveProperty<float> Cooldown = new ReactiveProperty<float>(0);

		public WeaponBehavior(WeaponSetting weaponSetting, ProjectileView.Factory factory)
		{
			m_WeaponSetting = weaponSetting;
			m_ProjectileFactory = factory;
		}

		public void OnTick(Transform playerTransform, float deltaTime)
		{
			_cooldown -= deltaTime;
			Cooldown.Value = _cooldown / m_WeaponSetting.Cooldown;

			if (_cooldown <= 0)
			{
				Collider2D[] enemies = GetEnemies(playerTransform);
				if (enemies.Length > 0)
				{
					//UnityEngine.Debug.Log($"Found {_weaponSetting.WeaponName} Enemies {enemies.Length}");
					_cooldown = m_WeaponSetting.Cooldown;
					for (int i = 0; i < m_WeaponSetting.Projectiles; i++)
					{
						Spawn(playerTransform, enemies);
					}
				}
			}
		}

		public void Spawn(Transform playerTransform, Collider2D[] enemies)
		{
			Collider2D selectedTarget = enemies[Random.Range(0, enemies.Length)];
			m_ProjectileFactory.Create(playerTransform.position, selectedTarget.transform.position, m_WeaponSetting);
		}

		private Collider2D[] GetEnemies(Transform playerTransform)
		{
			return Physics2D.OverlapCircleAll(playerTransform.position, m_WeaponSetting.Range, _layerMask);
		}

		public class Factory : PlaceholderFactory<WeaponSetting, WeaponBehavior> { }
	}
}


