using Assets.Scripts.Weapons;
using Survivors.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class WeaponsPresenter : ITickable
	{
		private readonly WeaponsView m_WeaponView;
		private readonly WeaponsModel m_WeaponModel;
		private readonly Transform m_PlayerTransform;

		public WeaponsPresenter(WeaponsView view, WeaponsModel model, PlayerView playerView, CompositeDisposable disposables)
		{
			m_WeaponView = view;
			m_WeaponModel = model;
			m_PlayerTransform = playerView.transform;

			//Set up start weapon view.
			foreach (var weapon in m_WeaponModel.WeaponBehaviors)
			{
				var startWeapon = m_WeaponView.AddWeapon(weapon.WeaponSetting);
				weapon.Cooldown
					.Subscribe(x => startWeapon.UpdateSlider(x))
					.AddTo(startWeapon);
			}

			//Observe new weapons being added and create weapon view.
			m_WeaponModel.WeaponBehaviors
				.ObserveAdd()
				.Subscribe(x =>
				{
					var weaponView = m_WeaponView.AddWeapon(x.Value.WeaponSetting);
					x.Value.Cooldown
						.Subscribe(x => weaponView.UpdateSlider(x))
						.AddTo(weaponView);
				})
				.AddTo(disposables);

			//Observe and remove weapon views when weapon behavior was removed.
			m_WeaponModel.WeaponBehaviors
				.ObserveRemove()
				.Subscribe(x => m_WeaponView.RemoveWeapon(x.Value.WeaponSetting))
				.AddTo(disposables);
		}

		public void Tick()
		{
			foreach (var behavior in m_WeaponModel.WeaponBehaviors)
			{
				behavior.OnTick(m_PlayerTransform, UnityEngine.Time.deltaTime);
			}

		}
	}
}
