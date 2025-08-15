using System;
using JetBrains.Annotations;
using Survivors.Features.Contracts;
using UnityEngine;

namespace Survivors.Features.Enemies.Components
{
    public class EnemyView : MonoBehaviour, IEnemyView, ITakeDamage
    {
        [SerializeField] [CanBeNull] private SpriteRenderer _damageRenderer;
        [SerializeField] private float _damageFlickerDuration;

        public Transform Transform => transform;

        public int Id { get; set; }

        private float _indicatorCounter;
        private bool _showIndicator;

        private Action<int, float> _onTakeDamage;
        private Action<Vector2> _onDespawn;

        public void Initialize(
            int id,
            Action<int, float> onTakeDamage,
            Action<Vector2> onDespawn)
        {
            Id = id;
            _onTakeDamage = onTakeDamage;
            _onDespawn = onDespawn;
        }

        public void TakeDamage(float damage)
        {
            _onTakeDamage?.Invoke(Id, -damage);
            _indicatorCounter = 0;
            _showIndicator = true;
            SetDamageIndicator(_showIndicator);
        }

        public void OnDespawn()
        {
            _onDespawn?.Invoke(transform.position);
        }

        public void OnTick()
        {
            if (!_showIndicator) return;

            _indicatorCounter += Time.deltaTime;

            if (_indicatorCounter >= _damageFlickerDuration)
            {
                _showIndicator = false;
                SetDamageIndicator(_showIndicator);
            }
        }

        public void UpdatePositionAndRotation(Vector2 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public void SetState(bool state)
        {
            if (gameObject.activeSelf != state)
                gameObject.SetActive(state);
        }

        public void SetDamageIndicator(bool state)
        {
            if (_damageRenderer != null) _damageRenderer.enabled = state;
        }

        public void Dispose()
        {
            _onDespawn = null;
            _onTakeDamage = null;
        }
    }
}