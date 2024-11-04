using Survivors.Data;
using Survivors.Enemy;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallGameStateExtension
	{
		public static DiContainer InstallGameState(this DiContainer container)
		{
			container.Bind<PlayerData>().AsSingle();
			container.Bind<GameState>().AsSingle().NonLazy();

			return container;
		}
	}
}
