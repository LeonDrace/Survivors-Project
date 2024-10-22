using Scripts;
using Survivors.Data;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Survivors.Player
{
	public class PlayerModel
	{
		private readonly PlayerSettings _settings;
		private readonly PlayerData _data;

		public ReactiveProperty<float> CurrentHealth => _data.CurrentHealth;
		public ReactiveProperty<float> CurrentHealthPercentage => _data.CurrentHealthPercentage;
		public ReactiveProperty<bool> IsDead => _data.IsDead;
		public ReactiveCollection<WeaponSetting> EquippedWeapons => _data.EquippedWeapons;
		public float DamageFlickerDuration { get; set; }

		public PlayerModel(PlayerSettings settings, PlayerData playerData, CompositeDisposable disposables)
		{
			_settings = settings;
			_data = playerData;

			DamageFlickerDuration = settings.DamageFlickerDuration;
			_data.CurrentHealth = new ReactiveProperty<float>(settings.Health);
			_data.CurrentHealthPercentage = new ReactiveProperty<float>(1);
			_data.IsDead = new ReactiveProperty<bool>(false);
			_data.EquippedWeapons = new ReactiveCollection<WeaponSetting>
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


