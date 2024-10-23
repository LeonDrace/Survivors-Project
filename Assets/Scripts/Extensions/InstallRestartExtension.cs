using Survivors.UI;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallRestartExtension
	{
		public static DiContainer InstallRestart(this DiContainer container)
		{
			container.Bind<RestartScreenModel>().AsSingle();
			container.Bind<RestartScreenPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
