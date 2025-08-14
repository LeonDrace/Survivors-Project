using UnityEngine;

namespace Survivors.Scripts.Enemies.Settings
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "Survivors/Spawn Settings")]
    public class SpawnSettings : ScriptableObject
    {
        [field: SerializeField] private Vector2 SpawnIntervalRange { get; set; } = new(0.5f, 1.5f);
        [field: SerializeField] private Vector2Int SpawnAmountRange { get; set; } = new(1, 5);
        [field: SerializeField] public int MaxSpawnAmount { get; private set; }
        [field: SerializeField] public Vector2Int SpawnOffset { get; set; } = new(100, 100);


        public int GetSpawnAmount()
        {
            return Random.Range(SpawnAmountRange.x, SpawnAmountRange.y);
        }

        public float GetRandomSpawnCooldown()
        {
            return Random.Range(SpawnIntervalRange.x, SpawnIntervalRange.y);
        }
    }
}