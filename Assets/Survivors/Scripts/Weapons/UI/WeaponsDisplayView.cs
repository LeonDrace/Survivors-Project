using System.Collections.Generic;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsDisplayView : MonoBehaviour
	{
		[SerializeField]
		private Transform m_WeaponsContainer = null;
		public Transform Container => m_WeaponsContainer;

		private List<WeaponView> m_WeaponViews = new List<WeaponView>();
		public List<WeaponView> WeaponViews => m_WeaponViews;

		public void AddWeapon(WeaponView weaponView)
		{
			m_WeaponViews.Add(weaponView);
		}

		public void RemoveWeapon(WeaponView weaponView)
		{
			m_WeaponViews.Remove(weaponView);
		}
	}
}
