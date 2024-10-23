using Survivors.Player;
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
		public ReactiveProperty<float> CurrentHealth { get; private set; } = new ReactiveProperty<float>();
		public ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>(false);

		private EnemySettings m_Settings;
		private readonly PlayerPresenter m_Player;
		private readonly CompositeDisposable m_Disposables;

		public EnemyModel(PlayerView playerView, PlayerPresenter playerPresenter, CompositeDisposable disposables)
		{
			Target = playerView.transform;
			m_Player = playerPresenter;
			m_Disposables = disposables;
		}

		public void SetSettings(EnemySettings settings)
		{
			m_Settings = settings;
			CurrentHealth.Value = m_Settings.Health;
			CurrentHealth.Where(x => x <= 0).Subscribe(_ => IsDead.Value = true).AddTo(m_Disposables);
		}

		public void ResetCooldown()
		{
			Cooldown = m_Settings.DamageCooldown;
		}

		public void ResetInterval()
		{
			Interval = m_Settings.PathfindingInverval;
		}

		public void DealDamageToPlayer(float damage)
		{
			m_Player.DealDamge(damage);
		}
	}
}
