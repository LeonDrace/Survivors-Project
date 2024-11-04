using UniRx;
using UnityEngine;

namespace Survivors.Player
{
	public class PlayerModel : IPlayerHealthData
	{
		private readonly CharacterSettings m_Settings;
		public float BaseHealth => m_Settings.Health;
		public ReactiveProperty<float> CurrentHealth{ get; set; }
		public ReactiveProperty<float> CurrentHealthPercentage{ get; set; }
		public ReactiveProperty<bool> IsDead { get; set; }
		public float DamageFlickerDuration { get; set; }

		public PlayerModel(CharacterSettings settings, CompositeDisposable disposables)
		{
			m_Settings = settings;

			DamageFlickerDuration = settings.DamageFlickerDuration;
			CurrentHealth = new ReactiveProperty<float>(settings.Health);
			CurrentHealthPercentage = new ReactiveProperty<float>(1);
			IsDead = new ReactiveProperty<bool>(false);
		}
	}
}


