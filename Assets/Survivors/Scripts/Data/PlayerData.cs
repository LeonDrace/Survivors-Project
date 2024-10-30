using Survivors.Player;
using Survivors.Weapons;
using UniRx;
using UnityEngine;

namespace Survivors.Data
{
	public class PlayerData
	{
		public Transform Transform { get; }
		public ReactiveProperty<float> CurrentHealth { get; set; }
		public ReactiveProperty<float> CurrentHealthPercentage { get; set; }
		public ReactiveProperty<bool> IsDead { get; set; }
		public ReactiveCollection<WeaponBehaviorPresenter> EquippedWeapons { get; set; } = new ReactiveCollection<WeaponBehaviorPresenter>();

		public PlayerData(PlayerView playerView)
		{
			Transform = playerView.transform;
		}
	}
}
