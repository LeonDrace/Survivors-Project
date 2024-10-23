using Scripts;
using Survivors.Player;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using UnityEngine;
using Zenject;

namespace Survivors.Extensions
{
	public static class InstallPlayerExtension
	{
		public static DiContainer InstallPlayer(this DiContainer container, PlayerModel.PlayerSettings settings)
		{
			container.Bind<PlayerModel>().AsSingle().NonLazy();
			container.Bind<WeaponsModel>().AsSingle().NonLazy();
			container.Bind<PlayerPresenter>().AsSingle().NonLazy();
			container.BindInterfacesAndSelfTo<WeaponsPresenter>().AsSingle();
			container.BindFactory<WeaponSetting, WeaponBehavior, WeaponBehavior.Factory>();
			container.BindFactory<Vector2, Vector2, WeaponSetting, ProjectileView, ProjectileView.Factory>()
				.FromComponentInNewPrefab(settings.baseProjectilePrefab)
				.WithGameObjectName("Projectile")
				.UnderTransformGroup("Projectiles");

			return container;
		}
	}
}
