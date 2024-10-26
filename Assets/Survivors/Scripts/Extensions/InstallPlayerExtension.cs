using Survivors.Player;
using Survivors.Weapons;
using UnityEngine;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallPlayerExtension
	{
		public static DiContainer InstallPlayer(this DiContainer container, CharacterSettings settings)
		{
			container.Bind<PlayerModel>().AsSingle().NonLazy();
			container.Bind<WeaponsModel>().AsSingle().NonLazy();
			container.Bind<PlayerPresenter>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<WeaponsPresenter>().AsSingle();
			container.BindFactory<WeaponSetting, WeaponBehavior, WeaponBehavior.Factory>();
			container.Bind<ProjectileFactory>().AsSingle().NonLazy();

			return container;
		}
	}
}
