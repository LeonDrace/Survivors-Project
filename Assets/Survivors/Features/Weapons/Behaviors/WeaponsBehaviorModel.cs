using Survivors.Features.Contracts;
using Survivors.Features.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Features.Weapons.Behaviors
{
    public class WeaponsBehaviorModel : IPlayerWeaponsData
    {
        private readonly WeaponBehaviorFactory m_Factory;

        public ReactiveCollection<WeaponBehaviorPresenter> EquippedWeapons { get; set; } = new();
        public Transform PlayerTransform { get; private set; }

        public WeaponsBehaviorModel(CharacterSettings settings,
            IPlayerTransformData playerTransformData,
            WeaponBehaviorFactory factory)
        {
            m_Factory = factory;
            PlayerTransform = playerTransformData.Transform;
            EquippedWeapons.Add(m_Factory.Create(settings.GetDefaultWeaponSetting()));
        }
    }
}