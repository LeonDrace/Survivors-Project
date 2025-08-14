
using Survivors.Features.Weapons.Projectiles;
using Survivors.Features.Weapons.Settings;

namespace Survivors.Features.Weapons.Behaviors
{
	public class WeaponBehaviorFactory
	{
		private readonly ProjectileFactory m_ProjectileFactory;

		public WeaponBehaviorFactory(ProjectileFactory projectileFactory)
		{
			m_ProjectileFactory = projectileFactory;
		}

		public WeaponBehaviorPresenter Create(WeaponSetting weaponSetting)
		{
			var model = new WeaponBehaviorModel(weaponSetting, m_ProjectileFactory);
			return new WeaponBehaviorPresenter(model);
		}
	}
}
