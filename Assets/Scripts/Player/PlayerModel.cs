using System.Linq;
using UniRx;
using UnityEngine;

namespace Scripts
{
	public class PlayerModel
	{
		private readonly PlayerSettings _settings;

		public ReactiveProperty<float> CurrentHealth { get; set; }
		public ReactiveProperty<float> CurrentHealthPercentage { get; set; }
		public ReactiveProperty<bool> IsDead { get; set; }
		public ReactiveCollection<WeaponSetting> EquippedWeapons { get; set; }
		public float DamageFlickerDuration { get; set; }

		public PlayerModel(PlayerSettings settings, CompositeDisposable disposables)
		{
			_settings = settings;
			DamageFlickerDuration = settings.DamageFlickerDuration;
			CurrentHealth = new ReactiveProperty<float>(settings.Health);
			CurrentHealthPercentage = new ReactiveProperty<float>(1);
			IsDead = new ReactiveProperty<bool>(false);
			EquippedWeapons = new ReactiveCollection<WeaponSetting>
			{
				_settings.GetDefaultWeaponSetting()
			};
			CurrentHealth.Where(x => x <= 0).Subscribe(_ => IsDead.Value = true).AddTo(disposables);
			CurrentHealth.Subscribe(x => CurrentHealthPercentage.Value = x / settings.Health).AddTo(disposables);
		}

		[System.Serializable]
		public class PlayerSettings
		{
			[field: SerializeField]
			public int Health { get; private set; } = 10;
			[field: SerializeField]
			public int StartWeaponIndex { get; private set; } = 0;
			[field: SerializeField]
			public GameObject baseProjectilePrefab { get; private set; }
			[field: SerializeField]
			public WeaponSetting[] Weapons { get; private set; }
			[field: SerializeField]
			public float DamageFlickerDuration { get; private set; }

			public WeaponSetting GetDefaultWeaponSetting()
			{
				return Weapons[StartWeaponIndex];
			}
		}
	}
}


