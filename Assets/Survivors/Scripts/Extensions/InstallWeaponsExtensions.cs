using Survivors.Player;
using Survivors.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallWeaponsExtensions
	{
		public static DiContainer InstallWeapons(this DiContainer container)
		{
			container.Bind<WeaponsBehaviorModel>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<WeaponsBehaviorPresenter>().AsSingle();
			container.Bind<WeaponBehaviorModel>().AsTransient();

			container.Bind<ProjectileFactory>().AsSingle().NonLazy();
			container.Bind<WeaponBehaviorFactory>().AsSingle().NonLazy();
			container.Bind<WeaponViewFactory>().AsSingle().NonLazy();

			container.Bind<WeaponsDisplayModel>().AsSingle().NonLazy();
			container.Bind<WeaponsDisplayPresenter>().AsSingle().NonLazy();

			return container;
		}
	}
}
