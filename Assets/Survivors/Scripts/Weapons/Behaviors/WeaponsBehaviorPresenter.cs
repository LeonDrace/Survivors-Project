using Survivors.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Survivors.Weapons
{
	public class WeaponsBehaviorPresenter : ITickable
	{
		private readonly WeaponsBehaviorModel m_Model;

		public WeaponsBehaviorPresenter(WeaponsBehaviorModel model)
		{
			m_Model = model;
		}

		public void Tick()
		{
			foreach (var behavior in m_Model.EquippedWeapons)
			{
				behavior.OnTick(m_Model.PlayerTransform, UnityEngine.Time.deltaTime);
			}

		}
	}
}
