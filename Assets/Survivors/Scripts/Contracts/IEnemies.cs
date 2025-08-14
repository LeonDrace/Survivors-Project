using Survivors.Enemy;
using UniRx;

namespace Survivors.Scripts.Contracts
{
    public interface IEnemies
    {
        public ReactiveProperty<int> KilledEnemies { get; }
    }
}