using Survivors.Features.Enemies.Core;
using Survivors.Features.UI.Kill_Count;
using Zenject;

namespace Survivors.Features.Extensions
{
    public static class InstallEnemyExtension
    {
        public static DiContainer InstallEnemy(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<EnemyComponentManager>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyPools>().AsSingle();
            container.Bind<EnemyKillCounterModel>().AsSingle();
            container.Bind<EnemyKillCounterPresenter>().AsSingle().NonLazy();

            return container;
        }
    }
}