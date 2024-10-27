using Survivors.Data;
using Survivors.Player;
using UniRx;
using UnityEngine;

namespace Survivors.Weapons
{
	public class WeaponsBehaviorModel
	{
		private readonly WeaponBehaviorFactory m_Factory;
		private readonly PlayerData m_PlayerData;

		public ReactiveCollection<WeaponBehaviorPresenter> WeaponBehaviors => m_PlayerData.EquippedWeapons;
		public Transform PlayerTransform => m_PlayerData.Transform;

		public WeaponsBehaviorModel(CharacterSettings settings, PlayerData playerData, WeaponBehaviorFactory factory)
		{
			m_Factory = factory;
			m_PlayerData = playerData;
			WeaponBehaviors.Add(m_Factory.Create(settings.GetDefaultWeaponSetting()));
		}
	}
}
