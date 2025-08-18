using Survivors.Features.Contracts;
using Survivors.Features.Player;
using Zenject;

namespace Survivors.Features.Extensions
{
    public static class InstallPlayerExtension
    {
        public static DiContainer InstallPlayer(this DiContainer container)
        {
            container.Bind(typeof(PlayerModel), typeof(IPlayerHealthData)).To<PlayerModel>().AsSingle().NonLazy();
            container.Bind(typeof(PlayerPresenter), typeof(IPlayerTransformData)).To<PlayerPresenter>().AsSingle()
                .NonLazy();

            return container;
        }
    }
}