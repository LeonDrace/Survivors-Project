using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Scripts.Enemies.Enemy
{
    public class EnemyModel
    {
        public readonly ReactiveProperty<bool> IsDead = new(false);
        private readonly IPlayerHealthData m_PlayerHealth;
        private readonly EnemySettings m_Settings;

        public EnemyModel(IPlayerTransformData playerTransformData,
            IPlayerHealthData playerHealthData,
            EnemySettings settings)
        {
            Target = playerTransformData.Transform;
            m_PlayerHealth = playerHealthData;
            m_Settings = settings;
            CurrentHealth.Value = m_Settings.Health;
        }

        public Transform Target { get; private set; }
        public float DamageCooldown { get; set; }
        public float Range => m_Settings.Range;
        public int Interval { get; set; }
        public float Damage => m_Settings.Damage;
        public float DamageFlickerCountdown { get; set; }
        public float DamageFlickerDuration => m_Settings.DamageFlickerDuration;
        public bool IsDamageFlickerActive { get; set; }
        public float Speed => m_Settings.Speed;
        public float StoppingDistance => m_Settings.StoppingDistance;
        public Sprite Sprite => m_Settings.Sprite;
        public Color Color => m_Settings.Color;
        public ReactiveProperty<float> CurrentHealth { get; } = new();

        public void ResetCooldown()
        {
            DamageCooldown = m_Settings.DamageCooldown;
        }

        public void ResetInterval()
        {
            Interval = m_Settings.PathfindingInterval;
        }

        public void DealDamageToPlayer(float damage)
        {
            m_PlayerHealth.CurrentHealth.Value -= damage;
        }
    }
}