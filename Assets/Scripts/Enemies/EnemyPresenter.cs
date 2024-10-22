using UnityEngine;
using Zenject;

namespace Survivors.Enemy
{
	public class EnemyPresenter : IEnemy
	{
		private readonly EnemyModel _model;
		private readonly EnemyView _view;

		public EnemyPresenter(Vector2 position, EnemyModel.Settings settings, EnemyModel model)
		{
			_model = model;
			_model.SetSettings(settings);
			_view = GameObject.Instantiate(settings.Prefab, position, Quaternion.identity).GetComponent<EnemyView>();
			_view.Construct(position, settings);
			_view.onDamage += OnDamage;
		}

		private void OnDamage(float value)
		{
			_model.CurrentHealth.Value -= value;
		}

		public void OnTick()
		{
			_model.Interval--;

			//Pathfinding update.
			if (_model.Interval <= 0)
			{
				_view.Agent.SetDestination(_model.Target.position);
			}

			//Attack when in range.
			if (Vector3.Distance(_model.Target.position, _view.transform.position) <= _model.Range)
			{
				if (_model.Cooldown <= 0)
				{
					_model.DealDamageToPlayer(_model.Damage);
					_model.ResetCooldown();
				}
				_model.Cooldown -= Time.deltaTime;
			}
		}

		public bool IsDead()
		{
			return _model.IsDead.Value;
		}

		public void Destroy()
		{
			_view.DestroySelf();
		}

		public class Factory : PlaceholderFactory<Vector2, EnemyModel.Settings, EnemyPresenter>
		{

		}
	}
}
