using Survivors.Data;
using Survivors.Weapons;
using System.Linq;
using UniRx;

namespace Survivors.Player
{
	public class PlayerModel
	{
		private readonly CharacterSettings m_Settings;
		private readonly PlayerData m_Data;

		public ReactiveProperty<float> CurrentHealth => m_Data.CurrentHealth;
		public ReactiveProperty<float> CurrentHealthPercentage => m_Data.CurrentHealthPercentage;
		public ReactiveProperty<bool> IsDead => m_Data.IsDead;
		public float DamageFlickerDuration { get; set; }

		public PlayerModel(CharacterSettings settings, PlayerData playerData, CompositeDisposable disposables)
		{
			m_Settings = settings;
			m_Data = playerData;

			DamageFlickerDuration = settings.DamageFlickerDuration;
			m_Data.CurrentHealth = new ReactiveProperty<float>(settings.Health);
			m_Data.CurrentHealthPercentage = new ReactiveProperty<float>(1);
			m_Data.IsDead = new ReactiveProperty<bool>(false);

			CurrentHealth.Where(x => x <= 0).Subscribe(_ => IsDead.Value = true).AddTo(disposables);
			CurrentHealth.Subscribe(x => CurrentHealthPercentage.Value = x / settings.Health).AddTo(disposables);
		}
	}
}


