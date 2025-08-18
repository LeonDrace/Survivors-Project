using JetBrains.Annotations;
using UnityEngine;

namespace Survivors.Features.Map
{
    [UsedImplicitly]
    public class MapFactory
    {
        private readonly MapSettings _settings;

        public MapFactory(MapSettings settings)
        {
            _settings = settings;
            CreateMap();
        }

        private void CreateMap()
        {
            Object.Instantiate(_settings.Prefab).transform.position = Vector3.zero;
        }
    }
}