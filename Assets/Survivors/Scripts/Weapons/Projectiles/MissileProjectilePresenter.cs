using Survivors.Enemy;
using UnityEngine;

namespace Survivors.Weapons
{
	public class MissileProjectilePresenter : IProjectile
	{
		private readonly ProjectileModel m_Model;
		private readonly ProjectileView m_View;

		public MissileProjectilePresenter(Vector3 startPos, Vector3 targetrPos, WeaponSetting weaponSetting)
		{
			m_Model = new ProjectileModel(weaponSetting);
			m_Model.StartPosition = startPos;
			m_Model.TargetPosition = targetrPos;
			m_View = GameObject.Instantiate(weaponSetting.ProjectilePrefab).GetComponent<ProjectileView>();
			OnSpawnProjectile();

			m_View.OnCollision += OnHit;
		}

		private void OnSpawnProjectile()
		{
			Vector3 randomPos = Random.insideUnitSphere * m_Model.WeaponSetting.ProjectileSpawnRadius;
			m_View.transform.position = new Vector3(
				randomPos.x + m_Model.StartPosition.x,
				randomPos.y + m_Model.StartPosition.y,
				0);
			m_Model.Dir = (m_Model.TargetPosition - m_Model.StartPosition).normalized;

			SpriteRenderer spriteRenderer = m_View.SpriteRenderer;
			spriteRenderer.color = m_Model.WeaponSetting.ProjectileColor;
			spriteRenderer.sprite = m_Model.WeaponSetting.ProjectileSprite;
			spriteRenderer.transform.localScale = m_Model.WeaponSetting.ProjectileSize;
		}

		public void OnTick()
		{
			MoveProjectile();
			Cooldown();
		}

		private void MoveProjectile()
		{
			if (m_Model.IsDead) return;

			Vector3 speed = m_Model.Dir * m_Model.WeaponSetting.ProjectileSpeed
				* Time.deltaTime * m_Model.WeaponSetting.SpeedCurve.Evaluate(m_Model.CurrentLifeTime / m_Model.LifeTime);
			m_View.transform.position = m_View.transform.position + speed;
		}

		private void Cooldown()
		{
			if (m_Model.IsDead) return;

			m_Model.CurrentLifeTime += Time.deltaTime;
			if (m_Model.CurrentLifeTime >= m_Model.LifeTime)
			{
				Destroy();
			}
		}

		public void OnHit(Collider2D collision)
		{
			collision.gameObject.GetComponent<EnemyView>().DealDamage(m_Model.WeaponSetting.Damage);
			Destroy();
		}

		public bool IsDead()
		{
			return m_Model.IsDead;
		}

		public void Destroy()
		{
			GameObject.Destroy(m_View.gameObject);
			m_Model.IsDead = true;
		}
	}
}
