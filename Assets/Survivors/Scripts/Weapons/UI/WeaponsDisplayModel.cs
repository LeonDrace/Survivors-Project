using Survivors.Data;
using UniRx;

namespace Survivors.Weapons
{
	public class WeaponsDisplayModel
	{
		private readonly PlayerData m_PlayerData;
		public ReactiveCollection<WeaponBehavior> WeaponBehaviors => m_PlayerData.EquippedWeapons;

		public WeaponsDisplayModel(PlayerData playerData, WeaponBehavior.Factory factory, CompositeDisposable disposables)
		{
			m_PlayerData = playerData;
		}
	}
}
