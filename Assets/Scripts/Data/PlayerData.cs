using Scripts;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Data
{
	public class PlayerData
	{
		public PlayerData(PlayerView playerView)
		{
			_transform = playerView.transform;
		}

		private readonly Transform _transform;
		public Transform Transform => _transform;
		public ReactiveProperty<float> CurrentHealth { get; set; }
		public ReactiveProperty<float> CurrentHealthPercentage { get; set; }
		public ReactiveProperty<bool> IsDead { get; set; }
		public ReactiveCollection<WeaponSetting> EquippedWeapons { get; set; }
	}
}
