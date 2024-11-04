
namespace Survivors.Data
{
	public class GameState
	{	public PlayerData PlayerData { get; }
		public GameState(PlayerData playerData)
		{
			PlayerData = playerData;
		}
	}
}
