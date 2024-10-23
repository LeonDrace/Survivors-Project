
namespace Survivors.Data
{
	public class GameState
	{
		public GameState(PlayerData playerData, EnemyData enemyData)
		{
			m_PlayerData = playerData;
			m_EnemyData = enemyData;
		}

		private readonly PlayerData m_PlayerData;
		public PlayerData PlayerData => m_PlayerData;

		private readonly EnemyData m_EnemyData;
		public EnemyData EnemyData => m_EnemyData;
	}
}
