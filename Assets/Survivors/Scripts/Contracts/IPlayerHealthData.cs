using UniRx;

namespace Survivors.Scripts.Contracts
{
    public interface IPlayerHealthData
    {
        public void ChangeHealth(float change);
        public ReactiveProperty<float> CurrentHealth { get; }
        public ReactiveProperty<float> CurrentHealthPercentage { get; }
        public ReactiveProperty<bool> IsDead { get; }
    }
}