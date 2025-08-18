using System;
using UnityEngine;
using UnityEngine.UI;

namespace Survivors.Features.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private SpriteRenderer _damageRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Slider HealthSlider => _healthSlider;
        public SpriteRenderer DamageRenderer => _damageRenderer;

        public void Move(Vector2 direction)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + direction * (_speed * Time.deltaTime));
        }
    }
}