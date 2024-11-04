using UniRx;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsDisplayModel
	{
		private readonly IPlayerWeaponsData m_WeaponData;
		private readonly WeaponViewFactory m_WeaponViewFactory;
		public ReactiveCollection<WeaponBehaviorPresenter> WeaponBehaviors => m_WeaponData.EquippedWeapons;

		public WeaponsDisplayModel(IPlayerWeaponsData weaponData, WeaponViewFactory weaponViewFactory, CompositeDisposable disposables)
		{
			m_WeaponData = weaponData;
			m_WeaponViewFactory = weaponViewFactory;
		}

		public WeaponView CreateWeaponView(WeaponBehaviorPresenter weaponBehavior, Transform container)
		{
			return m_WeaponViewFactory.Create(weaponBehavior, container);
		}
	}
}
