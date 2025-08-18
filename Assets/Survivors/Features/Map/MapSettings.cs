using UnityEngine;

namespace Survivors.Features.Map
{
    [CreateAssetMenu(fileName = "MapSettings", menuName = "Survivors/Map Settings")]
    public class MapSettings : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}