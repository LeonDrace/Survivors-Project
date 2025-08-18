using Survivors.Features.Weapons.Settings;
using UnityEngine;

namespace Survivors.Features.Player
{
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "Survivors/CharacterSettings")]
    public class CharacterSettings : ScriptableObject
    {
        [field: SerializeField, Header("Stats")]
        public int Health { get; private set; } = 10;

        [field: SerializeField] public float DamageFlickerDuration { get; private set; }

        [field: SerializeField, Header("Weapon")]
        public WeaponSetting StartWeapon { get; private set; }


        public WeaponSetting GetDefaultWeaponSetting()
        {
            return StartWeapon;
        }
    }
}