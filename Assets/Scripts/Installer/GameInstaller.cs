using Assets.Scripts.Enemies;
using Survivors.Enemy;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts
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
			//Misc
			Container.BindInstance(_disposer);

			//Player
			Container.Bind<PlayerModel>().AsSingle().NonLazy();
			Container.Bind<WeaponsModel>().AsSingle().NonLazy();
			Container.Bind<PlayerPresenter>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<WeaponsPresenter>().AsSingle();
			Container.BindFactory<WeaponSetting, WeaponBehavior, WeaponBehavior.Factory>();
			Container.BindFactory<Vector2, Vector2, WeaponSetting, ProjectileView, ProjectileView.Factory>()
				.FromComponentInNewPrefab(_playerSettings.baseProjectilePrefab)
				.WithGameObjectName("Projectile")
				.UnderTransformGroup("Projectiles");

			//Enemies
			Container.Bind<EnemiesModel>().AsSingle();
			Container.BindInterfacesAndSelfTo<EnemiesPresenter>().AsSingle();
			Container.Bind<EnemyModel>().AsTransient();
			Container.BindFactory<Vector2, EnemyModel.Settings, EnemyPresenter, EnemyPresenter.Factory>();
		}

		private void OnDestroy()
		{
			_disposer.Dispose();
		}
	}
}
