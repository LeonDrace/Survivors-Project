using Survivors.Player;
using UniRx;

namespace Survivors.UI
{
	public class RestartScreenModel
	{
		private readonly IPlayerHealthData m_PlayerHealthData;

		public ReactiveProperty<bool> IsDead => m_PlayerHealthData.IsDead;

		public RestartScreenModel(IPlayerHealthData playerHealthData)
		{
			m_PlayerHealthData = playerHealthData;
		}
	}
}
