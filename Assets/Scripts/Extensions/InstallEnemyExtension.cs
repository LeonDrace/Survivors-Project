using Survivors.Enemy;
using UnityEngine;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallEnemyExtension
	{
		public static DiContainer InstallEnemy(this DiContainer container)
		{
			container.Bind<EnemiesModel>().AsSingle();
			container.BindInterfacesAndSelfTo<EnemiesPresenter>().AsSingle();
			container.Bind<EnemyModel>().AsTransient();
			container.BindFactory<Vector2, EnemySettings, EnemyPresenter, EnemyPresenter.Factory>();
			container.Bind<EnemyKillCounterPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
