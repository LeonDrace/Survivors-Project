using Survivors.Player;
using Survivors.Weapons;
using UniRx;
using UnityEngine;

namespace Survivors.Data
{
	public class PlayerData
	{
		private readonly Transform m_Transform;
		public Transform Transform => m_Transform;
		public ReactiveProperty<float> CurrentHealth { get; set; }
		public ReactiveProperty<float> CurrentHealthPercentage { get; set; }
		public ReactiveProperty<bool> IsDead { get; set; }
		public ReactiveCollection<WeaponBehavior> EquippedWeapons { get; set; } = new ReactiveCollection<WeaponBehavior>();

		public PlayerData(PlayerView playerView)
		{
			m_Transform = playerView.transform;
		}
	}
}
