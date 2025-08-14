using Survivors.Enemy;
using Survivors.Scripts.Enemies;
using Survivors.Scripts.Enemies.Core;
using Zenject;

namespace Survivors.Extensions
{
    public static class InstallEnemyExtension
    {
        public static DiContainer InstallEnemy(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<EnemyComponentManager>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            container.Bind<EnemyKillCounterModel>().AsSingle();
            container.Bind<EnemyKillCounterPresenter>().AsSingle().NonLazy();

            return container;
        }
    }
}