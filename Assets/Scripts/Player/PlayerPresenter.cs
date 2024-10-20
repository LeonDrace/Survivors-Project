using System;
using UniRx;
using UnityEngine;

namespace Scripts
{
	public class PlayerPresenter
	{
		private readonly PlayerView _playerView;
		private readonly PlayerModel _playerModel;

		public PlayerPresenter(Joystick joystick, PlayerView playerView, PlayerModel playerModel, CompositeDisposable disposer)
		{
			_playerModel = playerModel;
			_playerView = playerView;

			//Move
			joystick.OnInput
				.TakeWhile(_ => !_playerModel.IsDead.Value)
				.Subscribe(playerView.Move)
				.AddTo(disposer);

			//Damage
			_playerModel.CurrentHealthPercentage
				.Subscribe(x =>
				{
					if (x < 1)
					{
						_playerView.DamageRenderer.enabled = true;

						Observable
							.Timer(TimeSpan.FromSeconds(_playerModel.DamageFlickerDuration))
							.Subscribe(x => { _playerView.DamageRenderer.enabled = false; })
							.AddTo(disposer);
					}

				})
				.AddTo(disposer);

			//Current health
			_playerModel.CurrentHealthPercentage
				.Subscribe(x => _playerView.HealthSlider.value = x);

			//Death
			_playerModel.IsDead
				.Where(isDead => isDead == true)
				.Subscribe(x =>
				{
					_playerView.RestartScreen.gameObject.SetActive(true);
				})
				.AddTo(disposer);
		}

		public void DealDamge(float damage)
		{
			_playerModel.CurrentHealth.Value -= damage;
		}

		public Transform GetPlayerTransform()
		{
			return _playerView.transform;
		}
	}
}