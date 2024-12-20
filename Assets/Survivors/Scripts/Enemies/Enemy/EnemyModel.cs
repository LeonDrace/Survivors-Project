﻿using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemyModel
	{
		public Transform Target { get; private set; }
		public float Cooldown { get; set; }
		public float Range => m_Settings.Range;
		public int Interval { get; set; }
		public float Damage => m_Settings.Damage;
		public float DamageFlickerCountdown { get; set; }
		public float DamageFlickerDuration => m_Settings.DamageFlickerDuration;
		public bool IsDamageFlickerActive { get; set; }
		public float Speed => m_Settings.Speed;
		public float StoppingDistance => m_Settings.StoppingDistance;
		public Sprite Sprite => m_Settings.Sprite;
		public Color Color => m_Settings.Color;
		public ReactiveProperty<float> CurrentHealth { get; private set; } = new ReactiveProperty<float>();
		public ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>(false);

		private readonly EnemySettings m_Settings;
		private readonly IPlayerHealthData m_PlayerHealth;
		private readonly CompositeDisposable m_Disposables;

		public EnemyModel(IPlayerTransformData playerTransformData, 
			IPlayerHealthData playerHealthData, 
			EnemySettings settings, 
			CompositeDisposable disposables)
		{
			Target = playerTransformData.Transform;
			m_PlayerHealth = playerHealthData;
			m_Disposables = disposables;
			m_Settings = settings;
			CurrentHealth.Value = m_Settings.Health;
		}

		public void ResetCooldown()
		{
			Cooldown = m_Settings.DamageCooldown;
		}

		public void ResetInterval()
		{
			Interval = m_Settings.PathfindingInterval;
		}

		public void DealDamageToPlayer(float damage)
		{
			m_PlayerHealth.CurrentHealth.Value -= damage;
		}
	}
}
