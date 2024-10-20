using Assets.Scripts.Weapons;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class WeaponsPresenter : ITickable
	{
		private readonly WeaponsView _weaponView;
		private readonly WeaponsModel _weaponModel;
		private readonly Transform _playerTransform;

		public WeaponsPresenter(WeaponsView view, WeaponsModel model, PlayerView playerView, CompositeDisposable disposables)
		{
			_weaponView = view;
			_weaponModel = model;
			_playerTransform = playerView.transform;

			//Set up start weapon view.
			foreach (var weapon in _weaponModel.WeaponBehaviors)
			{
				var startWeapon = _weaponView.AddWeapon(weapon.WeaponSetting);
				weapon.Cooldown
					.Subscribe(x => startWeapon.UpdateSlider(x))
					.AddTo(startWeapon);
			}

			//Observe new weapons being added and create weapon view.
			_weaponModel.WeaponBehaviors
				.ObserveAdd()
				.Subscribe(x =>
				{
					var weaponView = _weaponView.AddWeapon(x.Value.WeaponSetting);
					x.Value.Cooldown
						.Subscribe(x => weaponView.UpdateSlider(x))
						.AddTo(weaponView);
				})
				.AddTo(disposables);

			//Observe and remove weapon views when weapon behavior was removed.
			_weaponModel.WeaponBehaviors
				.ObserveRemove()
				.Subscribe(x => _weaponView.RemoveWeapon(x.Value.WeaponSetting))
				.AddTo(disposables);
		}

		public void Tick()
		{
			foreach (var behavior in _weaponModel.WeaponBehaviors)
			{
				behavior.OnTick(_playerTransform, UnityEngine.Time.deltaTime);
			}

		}
	}
}
