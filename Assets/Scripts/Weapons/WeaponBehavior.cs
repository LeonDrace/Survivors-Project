using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class WeaponBehavior
	{
		private readonly WeaponSetting _weaponSetting;
		private readonly ProjectileView.Factory _projectileFactory;

		private float _cooldown = 0;
		private LayerMask _layerMask = 1 << 6;

		public WeaponSetting WeaponSetting => _weaponSetting;
		public ReactiveProperty<float> Cooldown = new ReactiveProperty<float>(0);

		public WeaponBehavior(WeaponSetting weaponSetting, ProjectileView.Factory factory)
		{
			_weaponSetting = weaponSetting;
			_projectileFactory = factory;
		}

		public void OnTick(Transform playerTransform, float deltaTime)
		{
			_cooldown -= deltaTime;
			Cooldown.Value = _cooldown / _weaponSetting.Cooldown;

			if (_cooldown <= 0)
			{
				Collider2D[] enemies = GetEnemies(playerTransform);
				if (enemies.Length > 0)
				{
					//UnityEngine.Debug.Log($"Found {_weaponSetting.WeaponName} Enemies {enemies.Length}");
					_cooldown = _weaponSetting.Cooldown;
					for (int i = 0; i < _weaponSetting.Projectiles; i++)
					{
						Spawn(playerTransform, enemies);
					}
				}
			}
		}

		public void Spawn(Transform playerTransform, Collider2D[] enemies)
		{
			Collider2D selectedTarget = enemies[Random.Range(0, enemies.Length)];
			_projectileFactory.Create(playerTransform.position, selectedTarget.transform.position, _weaponSetting);
		}

		private Collider2D[] GetEnemies(Transform playerTransform)
		{
			return Physics2D.OverlapCircleAll(playerTransform.position, _weaponSetting.Range, _layerMask);
		}

		public class Factory : PlaceholderFactory<WeaponSetting, WeaponBehavior> { }
	}
}


