using UnityEngine;
using UnityEngine.UI;

namespace Survivors.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private SpriteRenderer _damageRenderer;

        public Slider HealthSlider => _healthSlider;
        public SpriteRenderer DamageRenderer => _damageRenderer;

        public void Move(Vector2 direction)
        {
            var oldPosition = transform.position;
            transform.position = Vector3.Lerp(oldPosition, oldPosition + (Vector3)direction * _speed,
                Time.deltaTime);
        }
    }
}