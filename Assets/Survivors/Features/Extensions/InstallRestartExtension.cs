using Survivors.Features.UI.Restart_Panel;
using Zenject;

namespace Survivors.Features.Extensions
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
