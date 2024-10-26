using UnityEngine;

namespace Survivors.Weapons
{
	public interface IProjectile
	{
		public void OnTick();
		public void OnHit(Collider2D collision);
		public bool IsDead();
		public void Destroy();
	}
}
