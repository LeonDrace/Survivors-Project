using UnityEngine;

namespace Scripts
{
	public abstract class WeaponSetting : ScriptableObject
	{
		[field: SerializeField]
		public string WeaponName { get; private set; } = "New";
		[field: SerializeField]
		public GameObject WeaponUiPrefab { get; private set; }
		[field: SerializeField]
		public float Damage { get; private set; } = 1;
		[field: SerializeField]
		public float Cooldown { get; private set; } = 1;
		[field: SerializeField]
		public float Range { get; private set; } = 8;
		[field: SerializeField]
		public float ProjectileSpawnRadius { get; private set; } = 1;
		[field: SerializeField]
		public Sprite ProjectileSprite { get; private set; }
		[field: SerializeField]
		public Color ProjectileColor { get; private set; }
		[field: SerializeField]
		public Vector2 ProjectileSize { get; private set; }
		[field: SerializeField]
		public float ProjectileSpeed { get; private set; }
		[field: SerializeField]
		public AnimationCurve SpeedCurve { get; private set; }
		[field: SerializeField]
		public float ProjectileLifeTime { get; private set; }
		[field: SerializeField]
		public int Projectiles { get; private set; }

		public abstract void OnSpawnProjectile(ProjectileView projectile);
		public abstract void OnMoveProjectile(ProjectileView projectile);
		public abstract void OnProjectileHit(ProjectileView projectile, Collider2D collision);
		public abstract void OnDestroyProjectile(ProjectileView projectile);
	}
}


