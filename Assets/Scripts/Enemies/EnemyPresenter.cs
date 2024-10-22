using UnityEngine;
using Zenject;

namespace Survivors.Enemy
{
	public class EnemyPresenter : IEnemy
	{
		private readonly EnemyModel m_Model;
		private readonly EnemyView m_View;

		public EnemyPresenter(Vector2 position, EnemyModel.Settings settings, EnemyModel model)
		{
			m_Model = model;
			m_Model.SetSettings(settings);
			m_View = GameObject.Instantiate(settings.Prefab, position, Quaternion.identity).GetComponent<EnemyView>();
			m_View.Construct(position, settings);
			m_View.onDamage += OnDamage;
		}

		private void OnDamage(float value)
		{
			m_Model.CurrentHealth.Value -= value;
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

		public class Factory : PlaceholderFactory<Vector2, EnemyModel.Settings, EnemyPresenter>
		{

		}
	}
}
