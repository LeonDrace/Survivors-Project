using Survivors.Features.Map;
using Zenject;

namespace Survivors.Features.Extensions
{
    public static class InstallMapExtension
    {
        public static DiContainer InstallMap(this DiContainer container)
        {
            container.Bind<MapFactory>().AsSingle().NonLazy();
            return container;
        }
    }
}