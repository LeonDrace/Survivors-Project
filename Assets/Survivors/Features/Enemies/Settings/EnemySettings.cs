using Survivors.Features.Contracts;
using Survivors.Features.Enemies.Components;
using UnityEngine;

namespace Survivors.Features.Enemies.Settings
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Survivors/Enemy Settings")]
    public class EnemySettings : ScriptableObject, ISharedEnemyData
    {
        //Meta
        [field: SerializeField] public string ConfigId { get; private set; } = "New";
        [field: SerializeField] public EnemyView Prefab { get; private set; }

        //Basic Stats
        [field: SerializeField] public float Health { get; private set; } = 3;
        [field: SerializeField] public float AttackRange { get; private set; } = 1;
        [field: SerializeField] public float Damage { get; private set; } = 1;
        [field: SerializeField] public float AttackSpeed { get; private set; } = 1;

        //Pathing
        [field: SerializeField] public float Speed { get; private set; } = 1.75f;
        [field: SerializeField] public float StoppingDistance { get; private set; } = 1;
        [field: SerializeField] public int PathfindingInterval { get; private set; } = 20;
    }
}