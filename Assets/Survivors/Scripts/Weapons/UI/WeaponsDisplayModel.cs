using Survivors.Data;
using UniRx;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsDisplayModel
	{
		private readonly PlayerData m_PlayerData;
		private readonly WeaponViewFactory m_WeaponViewFactory;
		public ReactiveCollection<WeaponBehaviorPresenter> WeaponBehaviors => m_PlayerData.EquippedWeapons;

		public WeaponsDisplayModel(PlayerData playerData, WeaponViewFactory weaponViewFactory, CompositeDisposable disposables)
		{
			m_PlayerData = playerData;
			m_WeaponViewFactory = weaponViewFactory;
		}

		public WeaponView CreateWeaponView(WeaponBehaviorPresenter weaponBehavior, Transform container)
		{
			return m_WeaponViewFactory.Create(weaponBehavior, container);
		}
	}
}
