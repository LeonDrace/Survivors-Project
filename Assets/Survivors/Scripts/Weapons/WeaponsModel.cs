using Survivors.Player;
using UniRx;

namespace Scripts
{
	public class WeaponsModel
	{
		private readonly WeaponBehavior.Factory m_Factory;

		public ReactiveCollection<WeaponBehavior> WeaponBehaviors = new ReactiveCollection<WeaponBehavior>();

		public WeaponsModel(PlayerModel playerModel, WeaponBehavior.Factory factory, CompositeDisposable disposables)
		{
			m_Factory = factory;
			foreach (var weapon in playerModel.EquippedWeapons)
			{
				WeaponBehaviors.Add(m_Factory.Create(weapon));
			}

			playerModel.EquippedWeapons
				.ObserveAdd()
				.Subscribe(weapon => WeaponBehaviors.Add(m_Factory.Create(weapon.Value)))
				.AddTo(disposables);

			playerModel.EquippedWeapons
				.ObserveRemove()
				.Subscribe(weapon =>
				{
					WeaponBehaviors.Remove(FindWeaponBehavior(weapon.Value));
				})
				.AddTo(disposables);
		}

		private WeaponBehavior FindWeaponBehavior(WeaponSetting weaponSetting)
		{
			for (int i = 0; i < WeaponBehaviors.Count; i++)
			{
				if (WeaponBehaviors[i].WeaponSetting == weaponSetting) return WeaponBehaviors[i];
			}
			return null;
		}
	}
}
