using Survivors.Weapons;
using UniRx;
using UnityEngine;

namespace Survivors.Player
{
    public interface IPlayerHealthData
    {
        public ReactiveProperty<float> CurrentHealth { get; set; }
        public ReactiveProperty<float> CurrentHealthPercentage { get; set; }
        public ReactiveProperty<bool> IsDead { get; set; }
    }
}