using UniRx;

namespace Survivors.Features.Contracts
{
    public interface IEnemies
    {
        public ReactiveProperty<int> KilledEnemies { get; }
    }
}