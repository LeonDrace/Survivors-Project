using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponViewFactory
	{
		public WeaponView Create(WeaponBehaviorPresenter weaponBehavior, Transform container)
		{
			var weaponView = GameObject.Instantiate(weaponBehavior.Model.WeaponUiPrefab, container).GetComponent<WeaponView>();
			weaponView.Initialize(weaponBehavior.Model.Guid,
				weaponBehavior.Model.ProjectileSprite, weaponBehavior.Model.ProjectileColor, weaponBehavior.Model.Name);
			return weaponView;
		}
	}
}
