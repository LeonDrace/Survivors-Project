using Survivors.Enemy;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallEnemyExtension
	{
		public static DiContainer InstallEnemy(this DiContainer container)
		{
			container.Bind(typeof(EnemiesModel), typeof(IEnemyData)).To<EnemiesModel>().AsSingle();
			container.BindInterfacesAndSelfTo<EnemiesPresenter>().AsSingle();
			container.Bind<EnemyModel>().AsTransient();
			container.Bind<EnemyFactory>().AsSingle().NonLazy();
			container.Bind<EnemyKillCounterPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
