using UniRx;

namespace Survivors.Features.Contracts
{
    public interface IPlayerHealthData
    {
        public void ChangeHealth(float change);
        public ReactiveProperty<float> CurrentHealth { get; }
        public ReactiveProperty<float> CurrentHealthPercentage { get; }
        public ReactiveProperty<bool> IsDead { get; }
    }
}