using UnityEngine;

namespace Survivors.Features.Weapons.Projectiles
{
	public class ProjectileView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer m_SpriteRenderer;
		public SpriteRenderer SpriteRenderer => m_SpriteRenderer;

		public event System.Action<Collider2D> OnCollision;

		public void OnTriggerEnter2D(Collider2D collision) => OnCollision?.Invoke(collision);
	}
}


