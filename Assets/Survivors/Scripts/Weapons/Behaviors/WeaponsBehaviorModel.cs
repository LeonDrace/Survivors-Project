using Survivors.Data;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsBehaviorModel
	{
		private readonly WeaponBehavior.Factory m_Factory;
		private readonly PlayerData m_PlayerData;

		public ReactiveCollection<WeaponBehavior> WeaponBehaviors => m_PlayerData.EquippedWeapons;
		public Transform PlayerTransform => m_PlayerData.Transform;

		public WeaponsBehaviorModel(CharacterSettings settings, PlayerData playerData, WeaponBehavior.Factory factory)
		{
			m_Factory = factory;
			m_PlayerData = playerData;
			WeaponBehaviors.Add(m_Factory.Create(settings.GetDefaultWeaponSetting()));
		}
	}
}
