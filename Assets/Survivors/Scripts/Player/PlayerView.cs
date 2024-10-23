using UnityEngine;
using UnityEngine.UI;

namespace Survivors.Player
{
	public class PlayerView : MonoBehaviour
	{
		[SerializeField, Range(float.Epsilon, 5f)]
		private float m_Speed;
		[SerializeField]
		private Slider m_HealthSlider;
		[SerializeField]
		private SpriteRenderer m_DamageRenderer;

		public Slider HealthSlider => m_HealthSlider;
		public SpriteRenderer DamageRenderer => m_DamageRenderer;

		public void Move(Vector2 direction)
		{
			var oldPosition = transform.position;
			transform.position = Vector3.Lerp(oldPosition, oldPosition + (Vector3)direction * m_Speed,
				Time.deltaTime);
		}

		public Vector2 Position
		{
			get => transform.position;
		}
	}
}