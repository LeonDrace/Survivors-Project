using Survivors.Enemy;
using UnityEngine;

namespace Scripts
{
	[CreateAssetMenu]
	public class MissileWeaponSetting : WeaponSetting
	{
		public override void OnSpawnProjectile(ProjectileView projectile)
		{
			Vector3 randomPos = Random.insideUnitSphere * ProjectileSpawnRadius;
			projectile.transform.position = new Vector3(
				randomPos.x + projectile.StartPosition.x,
				randomPos.y + projectile.StartPosition.y,
				0);
			projectile.Dir = (projectile.TargetPosition - projectile.StartPosition).normalized;

			SpriteRenderer spriteRenderer = projectile.SpriteRenderer;
			spriteRenderer.color = ProjectileColor;
			spriteRenderer.sprite = ProjectileSprite;
			spriteRenderer.transform.localScale = ProjectileSize;
		}

		public override void OnMoveProjectile(ProjectileView projectile)
		{
			Vector3 speed = projectile.Dir * ProjectileSpeed * Time.deltaTime * SpeedCurve.Evaluate(projectile.CurrentLifeTime / ProjectileLifeTime);
			projectile.transform.position = projectile.transform.position + speed;
		}

		public override void OnProjectileHit(ProjectileView projectile, Collider2D collision)
		{
			collision.gameObject.GetComponent<EnemyView>().DealDamage(Damage);
			OnDestroyProjectile(projectile);
		}

		public override void OnDestroyProjectile(ProjectileView projectile)
		{
			GameObject.Destroy(projectile.gameObject);
		}
	}
}


