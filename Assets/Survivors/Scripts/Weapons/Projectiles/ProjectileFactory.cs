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
					var view = GameObject.Instantiate(weaponSetting.ProjectilePrefab).GetComponent<ProjectileView>();
					var model = new ProjectileModel(startPos, targetPos, weaponSetting);
					return new MissileProjectilePresenter(model, view);
				default:
					return null;
			}
		}
	}
}
