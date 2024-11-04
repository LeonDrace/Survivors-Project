using UnityEngine;

namespace Survivors.Enemy
{
	[CreateAssetMenu(fileName = "SpawnSettings", menuName = "Survivors/Spawn Settings")]
	public class SpawnSettings : ScriptableObject
	{
		[field: SerializeField]
		private Vector2 SpawnIntervalRange { get; set; } = new UnityEngine.Vector2(0.5f, 1.5f);
		[field: SerializeField]
		private Vector2Int SpawnAmountRange { get; set; } = new Vector2Int(1, 5);
		[field: SerializeField]
		public float Radius { get; private set; } = 20;
		[field: SerializeField]
		public int MaxSpawnAmount { get; private set; }

		public int GetSpawnAmount() => UnityEngine.Random.Range(SpawnAmountRange.x, SpawnAmountRange.y);

		public float GetRandomSpawnCooldown() => UnityEngine.Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
	}
}
