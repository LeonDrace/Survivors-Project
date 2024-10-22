using Scripts;
using Survivors.Data;
using Survivors.Enemy;
using Survivors.Player;
using Survivors.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Installer
{
	public class GameInstaller : MonoInstaller<GameInstaller>
	{
		[Inject]
		private EnemiesPresenter.SpawnSettings _spawnSettings;
		[Inject]
		private PlayerModel.PlayerSettings _playerSettings;

		private readonly CompositeDisposable _disposer = new();

		public override void InstallBindings()
		{
			InstallMisc();
			InstallGameState();
			InstallPlayer();
			InstallEnemies();
			InstallRestartUI();
		}

		private void InstallMisc()
		{
			Container.BindInstance(_disposer);
		}

		private void InstallGameState()
		{
			Container.Bind<EnemyData>().AsSingle();
			Container.Bind<PlayerData>().AsSingle();
			Container.Bind<GameState>().AsSingle().NonLazy();
		}

		private void InstallPlayer()
		{
			Container.Bind<PlayerModel>().AsSingle().NonLazy();
			Container.Bind<WeaponsModel>().AsSingle().NonLazy();
			Container.Bind<PlayerPresenter>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<WeaponsPresenter>().AsSingle();
			Container.BindFactory<WeaponSetting, WeaponBehavior, WeaponBehavior.Factory>();
			Container.BindFactory<Vector2, Vector2, WeaponSetting, ProjectileView, ProjectileView.Factory>()
				.FromComponentInNewPrefab(_playerSettings.baseProjectilePrefab)
				.WithGameObjectName("Projectile")
				.UnderTransformGroup("Projectiles");
		}

		private void InstallEnemies()
		{
			Container.Bind<EnemiesModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemiesPresenter>().AsSingle();
			Container.Bind<EnemyModel>().AsTransient();
			Container.BindFactory<Vector2, EnemyModel.Settings, EnemyPresenter, EnemyPresenter.Factory>();
			Container.Bind<EnemyKillCounterPresenter>().AsSingle().NonLazy();
		}

		private void InstallRestartUI()
		{
			Container.Bind<RestartScreenModel>().AsSingle();
			Container.Bind<RestartScreenPresenter>().AsSingle().NonLazy();
		}

		private void OnDestroy()
		{
			_disposer.Dispose();
		}
	}
}
