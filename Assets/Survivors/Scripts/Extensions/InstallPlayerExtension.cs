using Survivors.Player;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallPlayerExtension
	{
		public static DiContainer InstallPlayer(this DiContainer container)
		{
			container.Bind(typeof(PlayerModel), typeof(IPlayerHealthData)).To<PlayerModel>().AsSingle().NonLazy();
			container.Bind(typeof(PlayerPresenter), typeof(IPlayerTransformData)).To<PlayerPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
