using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Survivors.Enemy
{
	[RequireComponent(typeof(NavMeshAgent), typeof(SpriteRenderer))]
	public class EnemyView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer m_SpriteRenderer;
		[SerializeField]
		private SpriteRenderer m_DamageRenderer;
		[SerializeField]
		private NavMeshAgent m_Agent;

		public NavMeshAgent Agent => m_Agent;
		public event Action<float> onDamage;

		private bool m_ShowsHitEffect = false;
		private EnemyModel.Settings m_Settings;

		public void Construct(Vector2 position, EnemyModel.Settings settings)
		{
			m_Settings = settings;
			transform.position = new Vector3(position.x, position.y, 0);
			m_SpriteRenderer.sprite = settings.Sprite;
			m_DamageRenderer.sprite = settings.Sprite;
			m_SpriteRenderer.color = settings.Color;
			m_Agent.speed = settings.Speed;
			m_Agent.stoppingDistance = settings.StoppingDistance;
			m_Agent.updateRotation = false;
			m_Agent.updateUpAxis = false;
		}

		public void DealDamage(float damage)
		{
			onDamage?.Invoke(damage);

			if (m_DamageRenderer != null && !m_ShowsHitEffect)
			{
				m_DamageRenderer.enabled = true;
				m_ShowsHitEffect = true;
				Observable
					.Timer(TimeSpan.FromSeconds(m_Settings.DamageFlickerDuration))
					.Subscribe(_ =>
					{
						m_DamageRenderer.enabled = false;
						m_ShowsHitEffect = false;
					})
					.AddTo(this);
			}
		}

		public void DestroySelf()
		{
			GameObject.Destroy(gameObject);
		}
	}
}
