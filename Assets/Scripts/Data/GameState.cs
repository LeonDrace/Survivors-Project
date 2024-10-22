
namespace Survivors.Data
{
	public class GameState
	{
		public GameState(PlayerData playerData, EnemyData enemyData)
		{
			_playerData = playerData;
			_enemyData = enemyData;
		}

		private readonly PlayerData _playerData;
		public PlayerData PlayerData => _playerData;

		private readonly EnemyData _enemyData;
		public EnemyData EnemyData => _enemyData;
	}
}
