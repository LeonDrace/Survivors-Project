using System;
using UniRx;
using UnityEngine;

namespace Survivors.Enemy
{
	public class EnemyPresenter : IEnemy
	{
		private readonly EnemyModel m_Model;
		private readonly EnemyView m_View;

		public EnemyPresenter(EnemyView view, EnemyModel model, CompositeDisposable disposables)
		{
			m_Model = model;
			m_View = view;
			m_View.SetAgent(m_Model.Speed, m_Model.StoppingDistance);
			m_View.SetVisuals(m_Model.Sprite, m_Model.Color);

			Observable
				.FromEvent<float>(h => m_View.OnDamage += OnDamage, h => m_View.OnDamage -= OnDamage)
				.Subscribe()
				.AddTo(disposables);

			m_Model.CurrentHealth
			.Where(x => x <= 0)
			.Subscribe(_ => m_Model.IsDead.Value = true)
			.AddTo(disposables);
		}

		private void OnDamage(float value)
		{
			m_Model.CurrentHealth.Value -= value;

			if (m_View.DamageRenderer != null && !m_Model.IsDamageFlickerActive)
			{
				m_View.SetDamageIndicator(true);
				m_Model.IsDamageFlickerActive = true;
				m_Model.DamageFlickerCountdown = m_Model.DamageFlickerDuration;
			}
		}

		public void OnTick()
		{
			Pathfind();
			DamageIndicator();
			TryAttack();
		}

		private void DamageIndicator()
		{
			if (!m_Model.IsDamageFlickerActive) return;

			m_Model.DamageFlickerCountdown -= Time.deltaTime;

			if (m_Model.DamageFlickerCountdown <= 0)
			{
				m_View.SetDamageIndicator(false);
				m_Model.IsDamageFlickerActive = false;
			}
		}

		private void Pathfind()
		{
			m_Model.Interval--;
			if (m_Model.Interval <= 0)
			{
				m_View.Agent.SetDestination(m_Model.Target.position);
				m_Model.ResetInterval();
			}
		}

		private void TryAttack()
		{
			if (Vector3.Distance(m_Model.Target.position, m_View.transform.position) <= m_Model.Range)
			{
				if (m_Model.Cooldown <= 0)
				{
					m_Model.DealDamageToPlayer(m_Model.Damage);
					m_Model.ResetCooldown();
				}
				m_Model.Cooldown -= Time.deltaTime;
			}
		}

		public bool IsDead()
		{
			return m_Model.IsDead.Value;
		}

		public void Destroy()
		{
			m_View.DestroySelf();
		}
	}
}
