using Survivors.Input;
using System;
using UniRx;
using UnityEngine;

namespace Survivors.Player
{
	public class PlayerPresenter : IPlayerTransformData
	{
		private readonly PlayerView m_View;
		private readonly PlayerModel m_Model;

		public Transform Transform => m_View.transform;

		public PlayerPresenter(Joystick joystick,
			PlayerView playerView, PlayerModel playerModel, CompositeDisposable disposables)
		{
			m_Model = playerModel;
			m_View = playerView;

			m_Model.CurrentHealth
				.Where(x => x <= 0)
				.Subscribe(_ => m_Model.IsDead.Value = true)
				.AddTo(disposables);

			m_Model.CurrentHealth
				.Subscribe(x => m_Model.CurrentHealthPercentage.Value = x / m_Model.BaseHealth)
				.AddTo(disposables);

			//Move
			joystick.OnInput
				.TakeWhile(_ => !m_Model.IsDead.Value)
				.Subscribe(playerView.Move)
				.AddTo(disposables);

			//Damage
			m_Model.CurrentHealthPercentage
				.Subscribe(x =>
				{
					if (x < 1)
					{
						m_View.DamageRenderer.enabled = true;

						Observable
							.Timer(TimeSpan.FromSeconds(m_Model.DamageFlickerDuration))
							.Subscribe(x => { m_View.DamageRenderer.enabled = false; })
							.AddTo(disposables);
					}

				})
				.AddTo(disposables);

			//Current health
			m_Model.CurrentHealthPercentage
				.Subscribe(x => m_View.HealthSlider.value = x);
		}
	}
}