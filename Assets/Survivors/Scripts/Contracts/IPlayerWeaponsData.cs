using Survivors.Weapons;
using UniRx;

namespace Survivors.Weapons
{
    public interface IPlayerWeaponsData
    {
        public ReactiveCollection<WeaponBehaviorPresenter> EquippedWeapons { get; set; }
    }
}