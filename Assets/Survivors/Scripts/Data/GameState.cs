
namespace Survivors.Data
{
	public class GameState
	{	public PlayerData PlayerData { get; }
		public EnemyData EnemyData { get; }
		public GameState(PlayerData playerData, EnemyData enemyData)
		{
			PlayerData = playerData;
			EnemyData = enemyData;
		}
	}
}
