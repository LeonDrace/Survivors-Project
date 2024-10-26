using UnityEngine;

namespace Survivors.Weapons
{
	public class ProjectileFactory
	{
		public IProjectile Create(Vector3 startPos, Vector3 targetPos, WeaponSetting weaponSetting)
		{
			switch (weaponSetting.ProjectileType)
			{
				case ProjectileType.Missile:
					return new MissileProjectilePresenter(startPos, targetPos, weaponSetting);
				default:
					return null;
			}
		}
	}
}
