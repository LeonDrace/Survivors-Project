using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponBehaviorModel
	{
		private readonly WeaponSetting m_WeaponSetting;
		private readonly ProjectileFactory m_ProjectileFactory;

		public Guid Guid { get; private set; } = System.Guid.NewGuid();
		public LayerMask LayerMask { get; private set; } = 1 << 6;
		public List<IProjectile> Projectiles { get; set; } = new List<IProjectile>();
		public float CurrentCooldown { get; set; } = 0;
		public float DefaultCooldown => m_WeaponSetting.Cooldown;
		public ReactiveProperty<float> CooldownPercentage { get; set; } = new ReactiveProperty<float>(0);
		public int ProjectileAmount => m_WeaponSetting.Projectiles;
		public Sprite ProjectileSprite => m_WeaponSetting.ProjectileSprite;
		public Color ProjectileColor => m_WeaponSetting.ProjectileColor;
		public string Name => m_WeaponSetting.WeaponName;
		public GameObject WeaponUiPrefab => m_WeaponSetting.WeaponUiPrefab;
		public float Range => m_WeaponSetting.Range;

		public WeaponBehaviorModel(WeaponSetting weaponSetting, ProjectileFactory projectileFactory)
		{
			m_WeaponSetting = weaponSetting;
			m_ProjectileFactory = projectileFactory;
		}

		public IProjectile CreateProjectile(Vector3 spawnPos, Vector3 targetPos)
		{
			return m_ProjectileFactory.Create(spawnPos, targetPos, m_WeaponSetting);
		}
	}
}
