using Survivors.Weapons;
using UniRx;

namespace Survivors.Scripts.Contracts
{
    public interface IPlayerWeaponsData
    {
        public ReactiveCollection<WeaponBehaviorPresenter> EquippedWeapons { get; set; }
    }
}