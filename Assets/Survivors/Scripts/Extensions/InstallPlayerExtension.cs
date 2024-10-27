using Survivors.Player;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallPlayerExtension
	{
		public static DiContainer InstallPlayer(this DiContainer container)
		{
			container.Bind<PlayerModel>().AsSingle().NonLazy();
			container.Bind<PlayerPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
