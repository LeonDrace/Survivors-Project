using Survivors.Player;
using Survivors.Weapons;
using UnityEngine;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallPlayerExtension
	{
		public static DiContainer InstallPlayer(this DiContainer container)
		{
			container.Bind<PlayerModel>().AsSingle().NonLazy();
			container.Bind<PlayerPresenter>().AsSingle().NonLazy();

			container.Bind<WeaponsBehaviorModel>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<WeaponsBehaviorPresenter>().AsSingle();
			container.BindFactory<WeaponSetting, WeaponBehavior, WeaponBehavior.Factory>();

			container.Bind<ProjectileFactory>().AsSingle().NonLazy();

			container.Bind<WeaponsDisplayModel>().AsSingle().NonLazy();
			container.Bind<WeaponsDisplayPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
