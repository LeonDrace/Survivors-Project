using UnityEngine;

namespace Survivors.Weapons
{
	public class ProjectileModel
	{
		private readonly WeaponSetting m_WeaponSetting;
		public bool IsDead { get; set; }
		public float CurrentLifeTime { get; set; }
		public float LifeTime => m_WeaponSetting.ProjectileLifeTime;
		public float SpawnRadius => m_WeaponSetting.ProjectileSpawnRadius;
		public float Damage => m_WeaponSetting.Damage;
		public Sprite Sprite => m_WeaponSetting.ProjectileSprite;
		public Vector2 Size => m_WeaponSetting.ProjectileSize;
		public Color Color => m_WeaponSetting.ProjectileColor;
		public float Speed => m_WeaponSetting.ProjectileSpeed;
		public AnimationCurve AnimationCurve => m_WeaponSetting.SpeedCurve;
		public Vector2 StartPosition { get; set; }
		public Vector2 TargetPosition { get; set; }
		public Vector3 Dir { get; set; }

		public ProjectileModel(Vector3 startPos, Vector3 targetPos, WeaponSetting settings)
		{
			m_WeaponSetting = settings;
			StartPosition = startPos;
			TargetPosition = targetPos;
		}
	}
}
