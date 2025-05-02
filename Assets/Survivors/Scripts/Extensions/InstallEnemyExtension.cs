using Survivors.Enemy;
using Survivors.Scripts.Enemies;
using Survivors.Scripts.Enemies.Enemy;
using Zenject;

namespace Survivors.Extensions
{
    public static class InstallEnemyExtension
    {
        public static DiContainer InstallEnemy(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<EnemiesContainer>().AsSingle();
            container.Bind<EnemyModel>().AsTransient();
            container.Bind<EnemyFactory>().AsSingle().NonLazy();
            container.Bind<EnemyKillCounterModel>().AsSingle();
            container.Bind<EnemyKillCounterPresenter>().AsSingle().NonLazy();

            return container;
        }
    }
}