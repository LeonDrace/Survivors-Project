using Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
	public class WeaponsView : MonoBehaviour
	{
		[SerializeField]
		private Transform weaponsContainer = null;

		private List<WeaponView> _weaponViews = new List<WeaponView>();

		public WeaponView AddWeapon(WeaponSetting weaponSetting)
		{
			var weaponView = Instantiate(weaponSetting.WeaponUiPrefab, weaponsContainer).GetComponent<WeaponView>();
			weaponView.Initialize(weaponSetting);
			_weaponViews.Add(weaponView);
			return weaponView;
		}

		public void RemoveWeapon(WeaponSetting weaponSetting)
		{
			_weaponViews.Remove(GetWeapon(weaponSetting));
		}

		private WeaponView GetWeapon(WeaponSetting weaponSetting)
		{
			return _weaponViews.Find(x => x.WeaponSetting == weaponSetting);
		}
	}
}
