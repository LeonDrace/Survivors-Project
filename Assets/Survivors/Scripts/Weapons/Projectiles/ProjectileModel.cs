using UnityEngine;

namespace Survivors.Weapons
{
	public class ProjectileModel
	{
		public WeaponSetting WeaponSetting { get; private set; }
		public bool IsDead { get; set; }
		public float CurrentLifeTime { get; set; }
		public float LifeTime => WeaponSetting.ProjectileLifeTime;
		public Vector2 StartPosition { get; set; }
		public Vector2 TargetPosition { get; set; }
		public Vector3 Dir { get; set; }

		public ProjectileModel(WeaponSetting settings)
		{
			WeaponSetting = settings;
		}
	}
}
