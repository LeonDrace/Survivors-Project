using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Survivors.Enemy
{
	[RequireComponent(typeof(NavMeshAgent), typeof(SpriteRenderer))]
	public class EnemyView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;
		[SerializeField]
		private SpriteRenderer _damageRenderer;
		[SerializeField]
		private NavMeshAgent _agent;

		public NavMeshAgent Agent => _agent;
		public event Action<float> onDamage;

		private bool _showsHitEffect = false;
		private EnemyModel.Settings _settings;

		public void Construct(Vector2 position, EnemyModel.Settings settings)
		{
			_settings = settings;
			transform.position = new Vector3(position.x, position.y, 0);
			_spriteRenderer.sprite = settings.Sprite;
			_damageRenderer.sprite = settings.Sprite;
			_spriteRenderer.color = settings.Color;
			_agent.speed = settings.Speed;
			_agent.stoppingDistance = settings.StoppingDistance;
			_agent.updateRotation = false;
			_agent.updateUpAxis = false;
		}

		public void DealDamage(float damage)
		{
			onDamage?.Invoke(damage);

			if (_damageRenderer != null && !_showsHitEffect)
			{
				_damageRenderer.enabled = true;
				_showsHitEffect = true;
				Observable
					.Timer(TimeSpan.FromSeconds(_settings.DamageFlickerDuration))
					.Subscribe(_ =>
					{
						_damageRenderer.enabled = false;
						_showsHitEffect = false;
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
