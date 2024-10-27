using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Enemy
{
	public class EnemyPresenter : IEnemy
	{
		private readonly EnemyModel m_Model;
		private readonly EnemyView m_View;

		public EnemyPresenter(EnemyView view, EnemyModel model)
		{
			m_Model = model;
			m_View = view;
			m_View.SetAgent(m_Model.Speed, m_Model.StoppingDistance);
			m_View.SetVisuals(m_Model.Sprite, m_Model.Color);
			m_View.onDamage += OnDamage;
		}

		private void OnDamage(float value)
		{
			m_Model.CurrentHealth.Value -= value;

			if (m_View.DamageRenderer != null && !m_Model.IsDamageFlickerActive)
			{
				m_View.DamageRenderer.enabled = true;
				m_Model.IsDamageFlickerActive = true;
				Observable
					.Timer(TimeSpan.FromSeconds(m_Model.DamageFlickerDuration))
					.Subscribe(_ =>
					{
						m_View.DamageRenderer.enabled = false;
						m_Model.IsDamageFlickerActive = false;
					})
					.AddTo(m_View);
			}
		}

		public void OnTick()
		{
			m_Model.Interval--;

			//Pathfinding update.
			if (m_Model.Interval <= 0)
			{
				m_View.Agent.SetDestination(m_Model.Target.position);
			}

			//Attack when in range.
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
