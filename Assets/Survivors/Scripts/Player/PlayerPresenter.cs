using Survivors.Input;
using System;
using UniRx;
using UnityEngine;

namespace Survivors.Player
{
	public class PlayerPresenter
	{
		private readonly PlayerView m_View;
		private readonly PlayerModel m_Model;

		public PlayerPresenter(Joystick joystick,
			PlayerView playerView, PlayerModel playerModel, CompositeDisposable disposables)
		{
			m_Model = playerModel;
			m_View = playerView;

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

		public Transform GetPlayerTransform()
		{
			return m_View.transform;
		}
	}
}