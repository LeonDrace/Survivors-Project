using UnityEngine;

namespace Survivors.Enemy
{
	[CreateAssetMenu(fileName = "EnemySettings", menuName = "Survivors/Enemy Settings")]
	public class EnemySettings : ScriptableObject
	{
		[field: SerializeField]
		public string Name { get; private set; } = "New";
		[field: SerializeField]
		public GameObject Prefab { get; private set; }
		[field: SerializeField]
		public Color Color { get; private set; }
		[field: SerializeField]
		public Sprite Sprite { get; private set; }
		[field: SerializeField]
		public float Health { get; private set; } = 3;
		[field: SerializeField]
		public float StoppingDistance { get; private set; } = 1;
		[field: SerializeField]
		public float Range { get; private set; } = 1;
		[field: SerializeField]
		public float Damage { get; private set; } = 1;
		[field: SerializeField]
		public float DamageCooldown { get; private set; } = 1;
		[field: SerializeField]
		public float Speed { get; private set; } = 1.75f;
		[field: SerializeField]
		public float DamageFlickerDuration { get; private set; } = 0.16f;
		[field: SerializeField]
		public int PathfindingInterval { get; private set; } = 20;
	}
}
