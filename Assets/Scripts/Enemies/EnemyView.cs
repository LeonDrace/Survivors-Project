using Scripts;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent), typeof(SpriteRenderer))]
public class EnemyView : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _spriteRenderer;
	[SerializeField]
	private SpriteRenderer _damageRenderer;
	[SerializeField]
	private NavMeshAgent _agent;
	private Transform _target;
	private Settings _settings;
	private float _currentHealth;

	private PlayerPresenter _player;

	private int _interval = 0;
	private int _intervalFrames = 20;
	private float _cooldown = 0;
	private IDisposable _damageTimerDisposable;

	public ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>(false);

	[Inject]
	public void Construct(Vector2 position, Settings settings, PlayerPresenter player)
	{
		transform.position = new Vector3(position.x, position.y, 0);
		_settings = settings;
		_player = player;
		_spriteRenderer.sprite = settings.sprite;
		_damageRenderer.sprite = settings.sprite;
		_spriteRenderer.color = settings.color;
		_target = player.GetPlayerTransform();
		_agent.speed = settings.Speed;
		_currentHealth = settings.Health;
		_agent.stoppingDistance = settings.StoppingDistance;
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
	}

	public void OnTick()
	{
		_interval--;

		//Pathfinding update.
		if (_interval <= 0)
		{
			_agent.SetDestination(_target.position);
			_interval = _intervalFrames;
		}

		//Attack when in range.
		if (Vector3.Distance(_target.position, transform.position) <= _settings.Range)
		{
			if (_cooldown <= 0)
			{
				_player.DealDamge(_settings.Damage);
				_cooldown = _settings.DamageCooldown;
			}
			_cooldown -= Time.deltaTime;
		}
	}

	public void DealDamage(float damage)
	{
		_currentHealth -= damage;

		if (_damageRenderer != null && _damageTimerDisposable == null)
		{
			_damageRenderer.enabled = true;
			_damageTimerDisposable = Observable
				.Timer(TimeSpan.FromSeconds(_settings.DamageFlickerDuration))
				.Subscribe(_ =>
				{
					_damageRenderer.enabled = false;
					_damageTimerDisposable = null;
				})
				.AddTo(this);
		}


		TryDestroySelf();
	}

	public void TryDestroySelf()
	{
		if (_currentHealth <= 0)
		{
			IsDead.Value = true;
		}
	}

	public void DestroySelf()
	{
		GameObject.Destroy(gameObject);
	}

	[System.Serializable]
	public class Settings
	{
		[field: SerializeField]
		public string name { get; private set; } = "New";
		[field: SerializeField]
		public Color color { get; private set; }
		[field: SerializeField]
		public Sprite sprite { get; private set; }
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
	}

	public class Factory : PlaceholderFactory<Vector2, Settings, EnemyView>
	{

	}
}
