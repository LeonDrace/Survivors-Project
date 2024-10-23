using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class ProjectileView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer m_SpriteRenderer;

		private WeaponSetting m_WeaponSetting;

		public SpriteRenderer SpriteRenderer => m_SpriteRenderer;
		public Vector2 StartPosition { get; set; }
		public Vector2 TargetPosition { get; set; }
		public Vector3 Dir { get; set; }
		public float CurrentLifeTime { get; set; }
		public Dictionary<string, object> CustomData { get; set; }

		[Inject]
		public void Construct(Vector2 startPosition, Vector2 targetPosition, WeaponSetting weaponSetting)
		{
			m_WeaponSetting = weaponSetting;
			StartPosition = startPosition;
			TargetPosition = targetPosition;

			m_WeaponSetting.OnSpawnProjectile(this);

			Observable
				.Timer(System.TimeSpan.FromSeconds(m_WeaponSetting.ProjectileLifeTime))
				.Subscribe(x =>
				{
					DestroyItself();
				})
				.AddTo(this);
		}

		public void Update()
		{
			CurrentLifeTime += Time.deltaTime;

			if (CurrentLifeTime >= m_WeaponSetting.ProjectileLifeTime)
			{
				DestroyItself();
				return;
			}

			m_WeaponSetting.OnMoveProjectile(this);
		}

		public void OnTriggerEnter2D(Collider2D collision) => m_WeaponSetting.OnProjectileHit(this, collision);

		public void DestroyItself() => m_WeaponSetting.OnDestroyProjectile(this);


		public class Factory : PlaceholderFactory<Vector2, Vector2, WeaponSetting, ProjectileView> { }
	}
}


