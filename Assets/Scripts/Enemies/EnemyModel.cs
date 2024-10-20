using Assets.Scripts.Enemies;
using Scripts;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Enemy
{
	public class EnemyModel
	{
		public Transform Target { get; private set; }
		public float Cooldown { get; set; }
		public float Range => _settings.Range;
		public int Interval { get; set; }
		public float Damage => _settings.Damage;
		public ReactiveProperty<float> CurrentHealth { get; private set; } = new ReactiveProperty<float>();
		public ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>(false);

		private Settings _settings;
		private readonly PlayerPresenter _player;
		private readonly CompositeDisposable _disposables;

		public EnemyModel(PlayerView playerView, PlayerPresenter playerPresenter, CompositeDisposable disposables)
		{
			Target = playerView.transform;
			_player = playerPresenter;
			_disposables = disposables;
		}

		public void SetSettings(Settings settings)
		{
			_settings = settings;
			CurrentHealth.Value = _settings.Health;
			CurrentHealth.Where(x => x <= 0).Subscribe(_ => IsDead.Value = true).AddTo(_disposables);
		}

		public void ResetCooldown()
		{
			Cooldown = _settings.DamageCooldown;
		}

		public void ResetInterval()
		{
			Interval = _settings.PathfindingInverval;
		}

		public void DealDamageToPlayer(float damage)
		{
			_player.DealDamge(damage);
		}

		[System.Serializable]
		public class Settings
		{
			[field: SerializeField]
			public string name { get; private set; } = "New";
			[field: SerializeField]
			public GameObject Prefab { get; private set; }
			[field: SerializeField]
			public Color Color { get; private set; }
			[field: SerializeField]
			public Sprite Sprite { get; private set; }
			[field: SerializeField]
			public float Health { get; private set; } = 3;
			[field: SerializeField]
			public float StoppingDistance { get; private set; } = 1;
			[field: SerializeField]
			public float Range { get; private set; } = 1;
			[field: SerializeField]
			public float Damage { get; private set; } = 1;
			[field: SerializeField]
			public float DamageCooldown { get; private set; } = 1;
			[field: SerializeField]
			public float Speed { get; private set; } = 1.75f;
			[field: SerializeField]
			public float DamageFlickerDuration { get; private set; } = 0.16f;
			[field: SerializeField]
			public int PathfindingInverval { get; private set; } = 20;
		}
	}
}
