using JetBrains.Annotations;
using Survivors.Scripts.Contracts;
using UnityEngine;

namespace Survivors.Scripts.Enemies.Components
{
    public class EnemyView : MonoBehaviour, IEnemyView, IDealDamage
    {
        [SerializeField] [CanBeNull] private SpriteRenderer _damageRenderer;
        [SerializeField] private float _damageFlickerDuration;

        private IEnemyManager _enemyManager;

        public Transform Transform => transform;
        public int Index { get; set; }

        private float _indicatorCounter;
        private bool _showIndicator;

        public void Initialize(IEnemyManager enemyManager, int index)
        {
            _enemyManager = enemyManager;
            Index = index;
        }

        public void DealDamage(float damage)
        {
            _enemyManager.ChangeHealth(Index, -damage);
            _indicatorCounter = 0;
            _showIndicator = true;
            SetDamageIndicator(_showIndicator);
        }

        public void Dispose()
        {
            Destroy(gameObject);
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

        public void SetDamageIndicator(bool state)
        {
            if (_damageRenderer != null) _damageRenderer.enabled = state;
        }
    }
}