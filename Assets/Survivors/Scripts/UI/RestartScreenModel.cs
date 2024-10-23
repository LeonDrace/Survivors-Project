

using Survivors.Data;
using UniRx;

namespace Survivors.UI
{
	public class RestartScreenModel
	{
		private readonly PlayerData m_PlayerData;

		public ReactiveProperty<bool> IsDead => m_PlayerData.IsDead;

		public RestartScreenModel(PlayerData playerData)
		{
			m_PlayerData = playerData;
		}
	}
}
