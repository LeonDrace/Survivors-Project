using Scripts;
using UnityEngine;

namespace Survivors.Player
{
	[CreateAssetMenu(fileName = "CharacterSettings", menuName = "Survivors/CharacterSettings")]
	public class CharacterSettings : ScriptableObject
	{
		[field: SerializeField, Header("Stats")]
		public int Health { get; private set; } = 10;
		[field: SerializeField]
		public float DamageFlickerDuration { get; private set; }

		[field: SerializeField, Header("Weapon")]
		public int StartWeaponIndex { get; private set; } = 0;
		[field: SerializeField]
		public GameObject baseProjectilePrefab { get; private set; }
		[field: SerializeField]
		public WeaponSetting[] Weapons { get; private set; }


		public WeaponSetting GetDefaultWeaponSetting()
		{
			return Weapons[StartWeaponIndex];
		}
	}
}