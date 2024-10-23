using Survivors.Data;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallGameStateExtension
	{
		public static DiContainer InstallGameState(this DiContainer container)
		{
			container.Bind<EnemyData>().AsSingle();
			container.Bind<PlayerData>().AsSingle();
			container.Bind<GameState>().AsSingle().NonLazy();

			return container;
		}
	}
}
