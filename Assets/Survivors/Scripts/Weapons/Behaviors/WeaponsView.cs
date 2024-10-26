using System.Collections.Generic;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsView : MonoBehaviour
	{
		[SerializeField]
		private Transform weaponsContainer = null;

		private List<WeaponView> m_WeaponViews = new List<WeaponView>();

		public WeaponView AddWeapon(WeaponSetting weaponSetting)
		{
			var weaponView = Instantiate(weaponSetting.WeaponUiPrefab, weaponsContainer).GetComponent<WeaponView>();
			weaponView.Initialize(weaponSetting);
			m_WeaponViews.Add(weaponView);
			return weaponView;
		}

		public void RemoveWeapon(WeaponSetting weaponSetting)
		{
			m_WeaponViews.Remove(GetWeapon(weaponSetting));
		}

		private WeaponView GetWeapon(WeaponSetting weaponSetting)
		{
			return m_WeaponViews.Find(x => x.WeaponSetting == weaponSetting);
		}
	}
}
