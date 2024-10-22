using Survivors.Input;
using System;
using UniRx;
using UnityEngine;

namespace Survivors.Player
{
	public class PlayerPresenter
	{
		private readonly PlayerView _view;
		private readonly PlayerModel _model;

		public PlayerPresenter(Joystick joystick,
			PlayerView playerView, PlayerModel playerModel, CompositeDisposable disposables)
		{
			_model = playerModel;
			_view = playerView;

			//Move
			joystick.OnInput
				.TakeWhile(_ => !_model.IsDead.Value)
				.Subscribe(playerView.Move)
				.AddTo(disposables);

			//Damage
			_model.CurrentHealthPercentage
				.Subscribe(x =>
				{
					if (x < 1)
					{
						_view.DamageRenderer.enabled = true;

						Observable
							.Timer(TimeSpan.FromSeconds(_model.DamageFlickerDuration))
							.Subscribe(x => { _view.DamageRenderer.enabled = false; })
							.AddTo(disposables);
					}

				})
				.AddTo(disposables);

			//Current health
			_model.CurrentHealthPercentage
				.Subscribe(x => _view.HealthSlider.value = x);
		}

		public void DealDamge(float damage)
		{
			_model.CurrentHealth.Value -= damage;
		}

		public Transform GetPlayerTransform()
		{
			return _view.transform;
		}
	}
}