using Survivors.Scripts.Contracts;
using UniRx;
using UnityEngine;

namespace Survivors.Scripts.Player
{
    public class PlayerModel : IPlayerHealthData
    {
        private readonly CharacterSettings _settings;
        public float BaseHealth => _settings.Health;

        public ReactiveProperty<float> CurrentHealth { get; }
        public ReactiveProperty<float> CurrentHealthPercentage { get; }
        public ReactiveProperty<bool> IsDead { get; }
        public float DamageFlickerDuration { get; }

        public PlayerModel(CharacterSettings settings)
        {
            _settings = settings;

            DamageFlickerDuration = settings.DamageFlickerDuration;
            CurrentHealth = new ReactiveProperty<float>(settings.Health);
            CurrentHealthPercentage = new ReactiveProperty<float>(1);
            IsDead = new ReactiveProperty<bool>(false);
        }

        public void ChangeHealth(float change)
        {
            var newHealth = CurrentHealth.Value + change;
            newHealth = Mathf.Clamp(newHealth, 0, _settings.Health);
            CurrentHealth.Value = newHealth;
        }
    }
}