using Survivors.Features.Weapons.Behaviors;
using UniRx;

namespace Survivors.Features.Contracts
{
    public interface IPlayerWeaponsData
    {
        public ReactiveCollection<WeaponBehaviorPresenter> EquippedWeapons { get; set; }
    }
}