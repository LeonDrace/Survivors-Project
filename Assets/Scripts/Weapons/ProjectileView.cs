using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class ProjectileView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		private WeaponSetting _weaponSetting;

		public SpriteRenderer SpriteRenderer => _spriteRenderer;
		public Vector2 StartPosition { get; set; }
		public Vector2 TargetPosition { get; set; }
		public Vector3 Dir { get; set; }
		public float CurrentLifeTime { get; set; }
		public Dictionary<string, object> CustomData { get; set; }

		[Inject]
		public void Construct(Vector2 startPosition, Vector2 targetPosition, WeaponSetting weaponSetting)
		{
			_weaponSetting = weaponSetting;
			StartPosition = startPosition;
			TargetPosition = targetPosition;

			_weaponSetting.OnSpawnProjectile(this);

			Observable
				.Timer(System.TimeSpan.FromSeconds(_weaponSetting.ProjectileLifeTime))
				.Subscribe(x =>
				{
					DestroyItself();
				})
				.AddTo(this);
		}

		public void Update()
		{
			CurrentLifeTime += Time.deltaTime;

			if (CurrentLifeTime >= _weaponSetting.ProjectileLifeTime)
			{
				DestroyItself();
				return;
			}

			_weaponSetting.OnMoveProjectile(this);
		}

		public void OnTriggerEnter2D(Collider2D collision) => _weaponSetting.OnProjectileHit(this, collision);

		public void DestroyItself() => _weaponSetting.OnDestroyProjectile(this);


		public class Factory : PlaceholderFactory<Vector2, Vector2, WeaponSetting, ProjectileView> { }
	}
}


