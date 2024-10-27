using Survivors.Enemy;
using UnityEngine;

namespace Survivors.Weapons
{
	public class MissileProjectilePresenter : IProjectile
	{
		private readonly ProjectileModel m_Model;
		private readonly ProjectileView m_View;

		public MissileProjectilePresenter(ProjectileModel model, ProjectileView view)
		{
			m_Model = model;
			m_View = view;
			OnSpawnProjectile();
		}

		private void OnSpawnProjectile()
		{
			Vector3 randomPos = Random.insideUnitSphere * m_Model.SpawnRadius;
			m_View.transform.position = new Vector3(
				randomPos.x + m_Model.StartPosition.x,
				randomPos.y + m_Model.StartPosition.y,
				0);
			m_Model.Dir = (m_Model.TargetPosition - m_Model.StartPosition).normalized;

			SpriteRenderer spriteRenderer = m_View.SpriteRenderer;
			spriteRenderer.color = m_Model.Color;
			spriteRenderer.sprite = m_Model.Sprite;
			spriteRenderer.transform.localScale = m_Model.Size;

			m_View.OnCollision += OnHit;
		}

		public void OnTick()
		{
			MoveProjectile();
			Cooldown();
		}

		private void MoveProjectile()
		{
			if (m_Model.IsDead) return;

			Vector3 speed = m_Model.Dir * m_Model.Speed
				* Time.deltaTime * m_Model.AnimationCurve.Evaluate(m_Model.CurrentLifeTime / m_Model.LifeTime);
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
			collision.gameObject.GetComponent<EnemyView>().DealDamage(m_Model.Damage);
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
