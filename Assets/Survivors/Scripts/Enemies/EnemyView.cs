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

		public SpriteRenderer DamageRenderer => m_DamageRenderer;
		public NavMeshAgent Agent => m_Agent;
		public event Action<float> OnDamage;

		public void SetAgent(float speed, float stoppingDistance)
		{
			m_Agent.speed = speed;
			m_Agent.stoppingDistance = stoppingDistance;
			m_Agent.updateRotation = false;
			m_Agent.updateUpAxis = false;
		}

		public void SetVisuals(Sprite sprite, Color color)
		{
			m_SpriteRenderer.sprite = sprite;
			m_DamageRenderer.sprite = sprite;
			m_SpriteRenderer.color = color;
		}

		public void DealDamage(float damage)
		{
			OnDamage?.Invoke(damage);
		}

		public void DestroySelf()
		{
			GameObject.Destroy(gameObject);
		}

		public void SetDamageIndicator(bool state)
		{
			m_DamageRenderer.enabled = state;
		}
	}
}
