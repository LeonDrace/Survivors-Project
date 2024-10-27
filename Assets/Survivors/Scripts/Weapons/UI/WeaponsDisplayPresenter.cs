using UniRx;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsDisplayPresenter
	{
		private readonly WeaponsDisplayModel m_Model;
		private readonly WeaponsDisplayView m_View;

		public WeaponsDisplayPresenter(WeaponsDisplayView view, WeaponsDisplayModel model, CompositeDisposable disposables)
		{
			m_View = view;
			m_Model = model;

			foreach (var weapon in m_Model.WeaponBehaviors)
			{
				AddWeaponView(weapon);
			}

			m_Model.WeaponBehaviors
				.ObserveAdd()
				.Subscribe(x =>
				{
					AddWeaponView(x.Value);
				})
				.AddTo(disposables);

			m_Model.WeaponBehaviors
				.ObserveRemove()
				.Subscribe(x => m_View.RemoveWeapon(m_View.WeaponViews.Find(x => x.Id == x.Id)))
				.AddTo(disposables);
		}

		private WeaponView AddWeaponView(WeaponBehaviorPresenter behavior)
		{
			var weaponView = m_Model.CreateWeaponView(behavior, m_View.Container);

			behavior.Model.CooldownPercentage
				.Subscribe(x => weaponView.UpdateSlider(x))
				.AddTo(weaponView);
			m_View.AddWeapon(weaponView);
			return weaponView;
		}
	}
}
