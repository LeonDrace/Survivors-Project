
namespace Survivors.Weapons
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
